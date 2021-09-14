using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class EditFormSettingsDescriptor : IFormGroupSettings
    {
        public string Title { get; set; }
        public EditFormRequestDetailsDescriptor RequestDetails { get; set; }
        public Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public EditType EditType { get; set; }
        public string ModelType { get; set; }
        public Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; set; }
        public HeaderBindingsDescriptor HeaderBindings { get; set; }
    }
}
