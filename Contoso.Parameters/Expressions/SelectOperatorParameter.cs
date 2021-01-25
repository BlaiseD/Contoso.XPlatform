namespace Contoso.Parameters.Expressions
{
    public class SelectOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public SelectOperatorParameter()
		{
		}

		public SelectOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}