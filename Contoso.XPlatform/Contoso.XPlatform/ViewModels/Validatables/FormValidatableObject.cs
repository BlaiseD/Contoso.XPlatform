using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class FormValidatableObject<T> : ValidatableObjectBase<T> where T : class
    {
        public FormValidatableObject(string name, FormGroupSettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            AddProperties(setting);
        }

        public ObservableCollection<IValidatable> Properties { get; set; } = new ObservableCollection<IValidatable>();

        private void AddProperties(FormGroupSettingsDescriptor setting)
        {
            throw new NotImplementedException();
        }
    }
}
