using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Globalization;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class LabelValidatableObject<T> : ValidatableObjectBase<T>
    {
        public LabelValidatableObject(string name, FormControlSettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService)
            : base(name, setting.TextTemplate.TemplateName, validations, uiNotificationService)
        {
            Placeholder = setting.Placeholder;
            Title = setting.Title;
            FormControlSettingsDescriptor = setting;
        }

        public FormControlSettingsDescriptor FormControlSettingsDescriptor { get; }

        public string DisplayText
        {
            get
            {
                if (EqualityComparer<T>.Default.Equals(Value, default(T)))
                    return string.Empty;

                if (string.IsNullOrEmpty(FormControlSettingsDescriptor.StringFormat))
                    return Value.ToString();

                return string.Format(CultureInfo.CurrentCulture, FormControlSettingsDescriptor.StringFormat, Value);
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private string _placeholder;
        public string Placeholder
        {
            get => _placeholder; set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }
    }
}
