using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using System;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public class EditFormEntityViewModel<TModel> : EditFormEntityViewModelBase where TModel : Domain.ViewModelBase
    {
        public EditFormEntityViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IHttpService httpService, IMapper mapper, IFieldsCollectionBuilder fieldsCollectionBuilder, IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder)
            : base(screenSettings, uiNotificationService, httpService, fieldsCollectionBuilder)
        {
            this.mapper = mapper;
            this.validateIfManager = new ValidateIfManager<TModel>
            (
                Properties,
                conditionalValidationConditionsBuilder.GetConditions<TModel>
                (
                    FormSettings.ConditionalDirectives,
                    Properties
                ),
                this.mapper,
                this.UiNotificationService
            );

            if (this.FormSettings.RequestDetails.EditType == EditType.Update)
                GetEntity();
        }

        private readonly ValidateIfManager<TModel> validateIfManager;
        private readonly IMapper mapper;
        private TModel entity;

        public override void Dispose()
        {
            base.Dispose();
            Dispose(this.validateIfManager);
        }

        private async void GetEntity()
        {
            if (this.FormSettings.RequestDetails.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 883E834F-98A6-4DF7-9D07-F1BB0D6639E1");

            GetEntityResponse getEntityResponse = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => httpService.GetEntity
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
                return;

            this.entity = (TModel)getEntityResponse.Entity;
            Properties.UpdateValidatables
            (
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings,
                this.mapper
            );
        }
    }
}
