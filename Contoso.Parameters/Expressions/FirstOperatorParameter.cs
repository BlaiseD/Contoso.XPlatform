namespace Contoso.Parameters.Expressions
{
    public class FirstOperatorParameter : FilterMethodOperatorParameterBase
    {
		public FirstOperatorParameter()
		{
		}

		public FirstOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public FirstOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}