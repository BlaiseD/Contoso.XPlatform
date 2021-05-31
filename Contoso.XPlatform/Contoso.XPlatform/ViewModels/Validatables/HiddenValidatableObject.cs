using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class HiddenValidatableObject<T> : ValidatableObjectBase<T>
    {
        public HiddenValidatableObject(string name, FormControlSettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) : base(name, setting.TextTemplate.TemplateName, validations, uiNotificationService)
        {
        }
    }
}
