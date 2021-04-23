using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.Validators
{
    public class PickerValidatableObject<T> : ValidatableObjectBase<T>
    {
        public PickerValidatableObject(string name, string templateName, string title, List<T> items, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) : base(name, templateName, validations, uiNotificationService)
        {
            this.Items = items;
            this.Title = title;
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

        private List<T> _items;
        public List<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectedIndexChangedCommand => new Command(() => IsValid = Validate());
    }
}
