namespace Contoso.Parameters.Expressions
{
    public class MaxOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public MaxOperatorParameter()
		{
		}

		public MaxOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}

		public MaxOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}