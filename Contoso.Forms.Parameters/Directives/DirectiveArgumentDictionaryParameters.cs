using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveArgumentDictionaryParameters : Dictionary<string, DirectiveArgumentParameters>
    {
        private List<DirectiveArgumentParameters> directiveArguments;

        public DirectiveArgumentDictionaryParameters
        (
            [Comments("List of arguments used by the directive. Directive examples: ValidateIf, HideIf")]
            List<DirectiveArgumentParameters> directiveArguments
        )
        {
            DirectiveArguments = directiveArguments;
        }

        public List<DirectiveArgumentParameters> DirectiveArguments
        {
            get => directiveArguments;
            set
            {
                directiveArguments = value;
                this.Clear();
                directiveArguments.ForEach(dad => this.Add(dad.Name, dad));
            }
        }
    }
}