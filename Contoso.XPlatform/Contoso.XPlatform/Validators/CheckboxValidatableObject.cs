﻿using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.Validators
{
    public class CheckboxValidatableObject : ValidatableObjectBase<bool>
    {
        public CheckboxValidatableObject(string name, string templateName, string checkboxLabel, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) : base(name, templateName, validations, uiNotificationService)
        {
            CheckboxLabel = checkboxLabel;
        }

        private string _checkboxLabel;
        public string CheckboxLabel
        {
            get => _checkboxLabel; 
            set
            {
                if (_checkboxLabel == value)
                    return;

                _checkboxLabel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckedChangedCommand => new Command(() => IsValid = Validate());
    }
}
