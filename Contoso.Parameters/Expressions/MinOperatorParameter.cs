namespace Contoso.Parameters.Expressions
{
    public class MinOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public MinOperatorParameter()
		{
		}

		public MinOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}

		public MinOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}