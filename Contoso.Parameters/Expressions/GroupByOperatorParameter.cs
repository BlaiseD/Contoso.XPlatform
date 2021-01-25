namespace Contoso.Parameters.Expressions
{
    public class GroupByOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public GroupByOperatorParameter()
		{
		}

		public GroupByOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}