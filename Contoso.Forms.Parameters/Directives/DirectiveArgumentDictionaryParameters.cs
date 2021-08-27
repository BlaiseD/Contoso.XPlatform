using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveArgumentDictionaryParameters : Dictionary<string, DirectiveArgumentParameters>
    {
		public DirectiveArgumentDictionaryParameters(List<DirectiveArgumentParameters> directiveArguments)
		{
			DirectiveArguments = directiveArguments;
		}

		public List<DirectiveArgumentParameters> DirectiveArguments { get; set; }
    }
}