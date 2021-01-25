namespace Contoso.Parameters.Expressions
{
    public class SelectManyOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public SelectManyOperatorParameter()
		{
		}

		public SelectManyOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
		}
    }
}