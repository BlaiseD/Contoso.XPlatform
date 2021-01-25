namespace Contoso.Parameters.Expressions
{
    public class CountOperatorParameter : FilterMethodOperatorParameterBase
    {
		public CountOperatorParameter()
		{
		}

		public CountOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public CountOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}