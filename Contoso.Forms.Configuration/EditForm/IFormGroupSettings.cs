using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public interface IFormGroupSettings
    {
        string ModelType { get; }
        string Title { get; }
        Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; }
        List<FormItemSettingsDescriptor> FieldSettings { get; }
        Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; }
    }
}
