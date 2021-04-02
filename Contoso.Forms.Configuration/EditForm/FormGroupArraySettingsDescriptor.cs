using Contoso.Forms.Configuration.Directives;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class FormGroupArraySettingsDescriptor : FormItemSettingsDescriptor
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.FormGroupArray;
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string ArrayElementType { get; set; }
        public List<string> KeyFields { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public VariableDirectivesDictionary ConditionalDirectives { get; set; }
    }
}
