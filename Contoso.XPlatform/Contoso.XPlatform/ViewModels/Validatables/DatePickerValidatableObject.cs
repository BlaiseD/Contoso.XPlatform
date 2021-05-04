﻿using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public class DatePickerValidatableObject : ValidatableObjectBase<DateTime>
    {
        public DatePickerValidatableObject(FormControlSettingsDescriptor setting, IEnumerable<IValidationRule> validations, UiNotificationService uiNotificationService) 
            : base(setting.Field, setting.TextTemplate.TemplateName, validations, uiNotificationService)
        {
        }

        public ICommand DateChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
