﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.Validators
{
    public class EntryValidatableObject : ValidatableObjectBase<string>
    {
        public EntryValidatableObject(string name, string templateName, string placeHolder, IEnumerable<IValidationRule> validations) : base(name, templateName, validations)
        {
            Placeholder = placeHolder;
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