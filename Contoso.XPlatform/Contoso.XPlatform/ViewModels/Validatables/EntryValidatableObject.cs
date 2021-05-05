using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class EntryValidatableObject<T> : ValidatableObjectBase<T>
    {
        public EntryValidatableObject(FormControlSettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(setting.Field, setting.TextTemplate.TemplateName, validations, uiNotificationService)
        {
            Placeholder = setting.Placeholder;
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

        public ICommand TextChangedCommand => new Command
        (
            async (parameter) =>
            {
                IsDirty = true;
                const int debounceDelay = 1000;
                string text = ((TextChangedEventArgs)parameter).NewTextValue;
                if (text == null)
                    return;

                await Task.Delay(debounceDelay).ContinueWith
                (
                    (task, oldText) =>
                    {
                        if (text == (string)oldText)
                            IsValid = Validate();
                    },
                    text
                );
            }
        );
    }
}
