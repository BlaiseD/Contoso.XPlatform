using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Directives
{
    public class VariableDirectivesParameters
    {
		public VariableDirectivesParameters
		(
			[Comments("")]
			string field,

			[Comments("")]
			List<DirectiveParameters> conditionalDirectives
		)
		{
			Field = field;
			ConditionalDirectives = conditionalDirectives;
		}

		public string Field { get; set; }
		public List<DirectiveParameters> ConditionalDirectives { get; set; }
    }
}