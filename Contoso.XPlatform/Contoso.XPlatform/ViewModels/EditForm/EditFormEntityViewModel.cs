using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public class EditFormEntityViewModel<TModel> : EditFormEntityViewModelBase where TModel : Domain.ViewModelBase
    {
        public EditFormEntityViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IMapper mapper, IHttpService httpService)
            : base(screenSettings, uiNotificationService, httpService, mapper)
        {
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                Properties,
                fieldsCollectionHelper.GetConditionalValidationConditions<TModel>
                (
                    FormSettings.ConditionalDirectives,
                    Properties,
                    mapper
                ),
                mapper,
                this.UiNotificationService
            );

            if (this.FormSettings.RequestDetails.EditType == EditType.Update)
                GetEntity();
        }

        private readonly ValidateIfManager<TModel> validateIfManager;

        public override void Dispose()
        {
            base.Dispose();
            Dispose(this.validateIfManager);
        }

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 883E834F-98A6-4DF7-9D07-F1BB0D6639E1");

            GetEntityResponse getEntityResponse = await BusyIndaicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => httpService.GetEntity
                (
                    new GetEntityRequest
                    {
                        Filter = this.FormSettings.RequestDetails.Filter,
                        SelectExpandDefinition = this.FormSettings.RequestDetails.SelectExpandDefinition,
                        ModelType = this.FormSettings.ModelType,
                        DataType = this.FormSettings.DataType
                    }
                )
            );

            if (getEntityResponse.Success == false)
                return;

            Properties.UpdateValidatables
            (
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings,
                App.ServiceProvider.GetRequiredService<IMapper>()
            );
        }
    }
}
