namespace Contoso.Parameters.Expressions
{
    public class MinOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public MinOperatorParameter()
		{
		}

		public MinOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}