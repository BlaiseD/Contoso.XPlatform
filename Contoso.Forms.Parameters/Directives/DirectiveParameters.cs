using Contoso.Parameters.Expressions;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveParameters
    {
		public DirectiveParameters(DirectiveDefinitionParameters definition, FilterLambdaOperatorParameters condition)
		{
			Definition = definition;
			Condition = condition;
		}

		public DirectiveDefinitionParameters Definition { get; set; }
		public FilterLambdaOperatorParameters Condition { get; set; }
    }
}