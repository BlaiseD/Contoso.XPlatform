using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class FormGroupSettingsDescriptor : FormItemSettingsDescriptor
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroup;
        public string Field { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public ValidationMessageDictionary ValidationMessages { get; set; }
        public VariableDirectivesDictionary ConditionalDirectives { get; set; }
    }
}
