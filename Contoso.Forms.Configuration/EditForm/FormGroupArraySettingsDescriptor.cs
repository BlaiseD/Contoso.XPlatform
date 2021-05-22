using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class FormGroupArraySettingsDescriptor : FormItemSettingsDescriptor, IFormGroupSettings
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroupArray;
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ModelType { get; set; }
        public List<string> KeyFields { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public ValidationMessageDictionary ValidationMessages { get; set; }
        public VariableDirectivesDictionary ConditionalDirectives { get; set; }
    }
}
