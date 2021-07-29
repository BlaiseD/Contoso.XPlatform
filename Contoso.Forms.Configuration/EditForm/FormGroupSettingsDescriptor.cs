﻿using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class FormGroupSettingsDescriptor : FormItemSettingsDescriptor, IFormGroupSettings
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroup;
        public string Title { get; set; }
        public string ValidFormControlText { get; set; }
        public string InvalidFormControlText { get; set; }
        public bool ShowTitle { get; set; }
        public string ModelType { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public ValidationMessageDictionary ValidationMessages { get; set; }
        public VariableDirectivesDictionary ConditionalDirectives { get; set; }
    }
}
