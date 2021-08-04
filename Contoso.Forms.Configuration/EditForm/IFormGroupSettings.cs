using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public interface IFormGroupSettings
    {
        string ModelType { get; }
        string Title { get; }
        string ValidFormControlText { get; }
        string InvalidFormControlText { get; }
        FormGroupTemplateDescriptor FormGroupTemplate { get;}
        VariableDirectivesDictionary ConditionalDirectives { get; }
        List<FormItemSettingsDescriptor> FieldSettings { get; }
        ValidationMessageDictionary ValidationMessages { get; }
    }
}
