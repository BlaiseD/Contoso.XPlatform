namespace Contoso.Parameters.Expressions
{
    public class MaxOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public MaxOperatorParameter()
		{
		}

		public MaxOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}