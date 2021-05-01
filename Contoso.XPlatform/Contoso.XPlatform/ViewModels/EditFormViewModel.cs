using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using System;

namespace Contoso.XPlatform.ViewModels
{
    public class EditFormViewModel<TModel> : EditFormViewModelBase, IDisposable
    {
        public EditFormViewModel()
        {
        }

        public EditFormViewModel(ScreenSettings<EditFormSettingsDescriptor> screenSettings, UiNotificationService uiNotificationService, IMapper mapper, IHttpService httpService)
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

        public void Dispose() 
            => Dispose(this.validateIfManager);

        private void Dispose(IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
