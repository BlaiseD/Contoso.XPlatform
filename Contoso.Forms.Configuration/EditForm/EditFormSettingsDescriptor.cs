using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class EditFormSettingsDescriptor : IFormGroupSettings
    {
        public string Title { get; set; }
        public string DisplayField { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public EditFormRequestDetailsDescriptor RequestDetails { get; set; }
        public ValidationMessageDictionary ValidationMessages { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public VariableDirectivesDictionary ConditionalDirectives { get; set; }
        public string ModelType { get; set; }//Helps Json (deserializer ??) on create
        public string DataType { get; set; }
        public string ValidFormControlText { get; set; }
        public string InvalidFormControlText { get; set; }
    }
}
