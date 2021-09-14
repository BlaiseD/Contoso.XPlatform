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
        public EditFormEntityViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, IContextProvider contextProvider)
            : base(screenSettings, contextProvider)
        {
            this.entityStateUpdater = contextProvider.EntityStateUpdater;
            this.httpService = contextProvider.HttpService;
            this.propertiesUpdater = contextProvider.PropertiesUpdater;
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                Properties,
                contextProvider.ConditionalValidationConditionsBuilder.GetConditions<TModel>
                (
                    FormSettings.ConditionalDirectives,
                    Properties
                ),
                contextProvider.Mapper,
                this.UiNotificationService
            );

            propertyChangedSubscription = this.UiNotificationService.ValueChanged.Subscribe(FieldChanged);

            if (this.FormSettings.EditType == EditType.Update)
                GetEntity();
        }

        private readonly IEntityStateUpdater entityStateUpdater;
        private readonly IHttpService httpService;
        private readonly IPropertiesUpdater propertiesUpdater;
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
                () => this.httpService.GetEntity
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

            this.propertiesUpdater.UpdateProperties
            (
                Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private ICommand _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??= new Command<CommandButtonDescriptor>
        (
            execute: async (button) =>
            {
                foreach (var property in Properties)
                    property.IsDirty = true;

                BaseResponse response = await this.httpService.SaveEntity
                (
                    new SaveEntityRequest<TModel> 
                    { 
                        Entity = this.entityStateUpdater.GetUpdatedModel
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
