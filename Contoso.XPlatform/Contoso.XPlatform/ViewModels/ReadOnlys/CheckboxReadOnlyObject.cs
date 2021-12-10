﻿using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class CheckboxReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public CheckboxReadOnlyObject(string name, FormControlSettingsDescriptor setting, IContextProvider contextProvider) : base(name, setting.TextTemplate.TemplateName, contextProvider.UiNotificationService)
        {
            FormControlSettingsDescriptor = setting;
            CheckboxLabel = FormControlSettingsDescriptor.Title;
        }

        public FormControlSettingsDescriptor FormControlSettingsDescriptor { get; }

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
    }
}
