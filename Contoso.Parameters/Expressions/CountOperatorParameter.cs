namespace Contoso.Parameters.Expressions
{
    public class CountOperatorParameter : FilterMethodOperatorParameterBase
    {
		public CountOperatorParameter()
		{
		}

		public CountOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}