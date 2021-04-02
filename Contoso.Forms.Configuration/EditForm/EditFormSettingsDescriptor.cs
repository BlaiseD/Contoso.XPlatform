using Contoso.Bsl.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class EditFormSettingsDescriptor
    {
        public string Title { get; set; }
        public string DisplayField { get; set; }
        public EditFormRequestDetailsDescriptor RequestDetails { get; set; }
        public ValidationMessageDictionary ValidationMessages { get; set; }
        public List<FormItemSettingsDescriptor> FieldSettings { get; set; }
        public FilterLambdaOperatorDescriptor Filter { get; set; }
        public VariableDirectivesDictionary ConditionalDirectives { get; set; }
        public string ModelType { get; set; }//Helps Json (deserializer ??) on create
    }
}
