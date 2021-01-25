namespace Contoso.Parameters.Expressions
{
    public class FirstOperatorParameter : FilterMethodOperatorParameterBase
    {
		public FirstOperatorParameter()
		{
		}

		public FirstOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}