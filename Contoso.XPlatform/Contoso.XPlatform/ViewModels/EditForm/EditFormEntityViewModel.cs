using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public class EditFormEntityViewModel<TModel> : EditFormEntityViewModelBase where TModel : Domain.ViewModelBase
    {
        public EditFormEntityViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IUtilities utilities)
            : base(screenSettings, uiNotificationService, utilities)
        {
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                Properties,
                this.utilities.ConditionalValidationConditionsBuilder.GetConditions<TModel>
                (
                    FormSettings.ConditionalDirectives,
                    Properties
                ),
                this.utilities.Mapper,
                this.UiNotificationService
            );

            propertyChangedSubscription = this.UiNotificationService.ValueChanged.Subscribe(FieldChanged);

            if (this.FormSettings.EditType == EditType.Update)
                GetEntity();
        }

        private readonly ValidateIfManager<TModel> validateIfManager;
        private TModel entity;
        private readonly IDisposable propertyChangedSubscription;

        public override void Dispose()
        {
            base.Dispose();
            Dispose(this.validateIfManager);
            Dispose(this.propertyChangedSubscription);
        }

        protected void Dispose(IDisposable disposable)
        {
            if (disposable != null)
                disposable.Dispose();
        }

        private void FieldChanged(string fieldName)
        {
            (SubmitCommand as Command).ChangeCanExecute();
        }

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 883E834F-98A6-4DF7-9D07-F1BB0D6639E1");

            GetEntityResponse getEntityResponse = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.utilities.HttpService.GetEntity
                (
                    new GetEntityRequest
                    {
                        Filter = this.FormSettings.RequestDetails.Filter,
                        SelectExpandDefinition = this.FormSettings.RequestDetails.SelectExpandDefinition,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType
                    }
                )
            );

            if (getEntityResponse.Success == false)
            {
                await App.Current.MainPage.DisplayAlert
                (
                    "Errors",
                    string.Join(Environment.NewLine, getEntityResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            this.entity = (TModel)getEntityResponse.Entity;

            Properties.UpdateValidatables
            (
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings,
                this.utilities.Mapper
            );
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??= new Command<CommandButtonDescriptor>
        (
            execute: async (button) =>
            {
                foreach (var property in Properties)
                    property.IsDirty = true;

                BaseResponse response = await this.utilities.HttpService.SaveEntity
                (
                    new SaveEntityRequest<TModel> 
                    { 
                        Entity = this.utilities.EntityStateUpdater.GetUpdatedModel
                        (
                            entity, 
                            Properties, 
                            FormSettings.FieldSettings
                        )
                    }
                );

                if (response.Success)
                    Next(button);
                else
                {
                    await App.Current.MainPage.DisplayAlert
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
                }
            },
            canExecute: (button) => AreFieldsValid()
        );
    }
}
