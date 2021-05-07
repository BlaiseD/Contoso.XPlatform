using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public class EditFormEntityViewModel<TModel> : EditFormEntityViewModelBase
    {
        public EditFormEntityViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IMapper mapper, IHttpService httpService)
            : base(screenSettings, uiNotificationService, httpService)
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
        }

        private readonly ValidateIfManager<TModel> validateIfManager;

        public override void Dispose()
        {
            base.Dispose();
            Dispose(this.validateIfManager);
        }
    }
}
