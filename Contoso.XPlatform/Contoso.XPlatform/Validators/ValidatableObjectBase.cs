using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Contoso.XPlatform.Validators
{
    public class ValidatableObjectBase<T> : IValidatable
    {
        public ValidatableObjectBase(string name, string templateName, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService)
        {
            Name = name;
            TemplateName = templateName;
            Validations = validations?.ToList();
            this.uiNotificationService = uiNotificationService;
        }

        #region Fields
        private T _value;
        private bool _isValid = true;
        private string _name;
        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private string _templateName;
        private UiNotificationService uiNotificationService;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Fields

        #region Properties
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                    return;

                _name = value;
                OnPropertyChanged();
            }
        }

        public string TemplateName
        {
            get => _templateName;
            set
            {
                if (_templateName == value)
                    return;

                _templateName = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            set
            {
                if (_isValid == value)
                    return;

                _isValid = value;
                OnPropertyChanged();
            }
        }

        public T Value
        {
            get => _value;
            set
            {
                if(EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;
                this.uiNotificationService.NotifyPropertyChanged();
                OnPropertyChanged();
            }
        }

        public Dictionary<string, string> Errors
        {
            get => _errors;
            set
            {
                if (_errors == value)
                    return;

                _errors = value;
                OnPropertyChanged();
            }
        }

        public List<IValidationRule> Validations { get; }

        object IValidatable.Value => Value;
        #endregion Properties

        public bool Validate()
        {
            Errors = Validations
                        .Where(v => !v.Check())
                        .ToDictionary(v => v.ClassName, v => v.ValidationMessage);

            IsValid = !Errors.Any();

            return this.IsValid;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
