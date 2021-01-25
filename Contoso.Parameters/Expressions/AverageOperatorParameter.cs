namespace Contoso.Parameters.Expressions
{
    public class AverageOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public AverageOperatorParameter()
		{
		}

		public AverageOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}

		public AverageOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}