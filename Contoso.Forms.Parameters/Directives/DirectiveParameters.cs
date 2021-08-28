using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveParameters
    {
		public DirectiveParameters
		(
			[Comments("")]
			DirectiveDefinitionParameters definition,

			[Comments("")]
			FilterLambdaOperatorParameters condition
		)
		{
			Definition = definition;
			Condition = condition;
		}

		public DirectiveDefinitionParameters Definition { get; set; }
		public FilterLambdaOperatorParameters Condition { get; set; }
    }
}