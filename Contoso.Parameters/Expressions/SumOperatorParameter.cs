namespace Contoso.Parameters.Expressions
{
    public class SumOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public SumOperatorParameter()
		{
		}

		public SumOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}