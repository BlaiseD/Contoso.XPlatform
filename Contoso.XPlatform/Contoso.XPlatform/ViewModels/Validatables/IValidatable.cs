﻿using Contoso.XPlatform.Validators;
using System.Collections.Generic;
using System.ComponentModel;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public interface IValidatable : INotifyPropertyChanged
    {
        string Name { get; set; }
        string TemplateName { get; set; }
        bool IsValid { get; set; }
        bool IsDirty { get; set; }
        object Value { get; }

        List<IValidationRule> Validations { get; }
        Dictionary<string, string> Errors { get; set; }

        bool Validate();
    }
}
