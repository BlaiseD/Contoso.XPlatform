using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public interface IFormGroupSettings
    {
        string ModelType { get; }
        VariableDirectivesDictionary ConditionalDirectives { get; }
        List<FormItemSettingsDescriptor> FieldSettings { get; }
        ValidationMessageDictionary ValidationMessages { get; }
    }
}
