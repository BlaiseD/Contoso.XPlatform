namespace Contoso.Parameters.Expressions
{
    public class AverageOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public AverageOperatorParameter()
		{
		}

		public AverageOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}