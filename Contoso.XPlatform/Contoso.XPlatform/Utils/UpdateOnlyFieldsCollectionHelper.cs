﻿using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Utils
{
    internal class UpdateOnlyFieldsCollectionHelper : FieldsCollectionHelper
    {
        public UpdateOnlyFieldsCollectionHelper(IFormGroupSettings formSettings, IContextProvider contextProvider, ObservableCollection<IValidatable> properties = null, string parentName = null) : base(formSettings, contextProvider, properties, parentName)
        {
        }

        protected override void AddFormControl(FormControlSettingsDescriptor setting)
        {
            if (setting.UpdateOnlyTextTemplate != null)
            {
                AddTextControl(setting, setting.UpdateOnlyTextTemplate);
            }
            else if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 32652CB4-2574-4E5B-9B3F-7E47B37425AD");
        }

        protected override void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            new FieldsCollectionHelper
            (
                setting,
                this.contextProvider,
                this.properties,
                GetFieldName(setting.Field)
            ).CreateFields();
        }
    }
}
