namespace Contoso.Parameters.Expressions
{
    public class SumOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public SumOperatorParameter()
		{
		}

		public SumOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}

		public SumOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}