﻿using System.Collections.Generic;
using System.ComponentModel;

namespace Contoso.XPlatform.Validators
{
    public interface IValidatable : INotifyPropertyChanged
    {
        string Name { get; set; }
        string TemplateName { get; set; }
        bool IsValid { get; set; }
        object Value { get; }

        List<IValidationRule> Validations { get; }
        Dictionary<string, string> Errors { get; set; }

        bool Validate();
    }
}