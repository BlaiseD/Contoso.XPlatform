namespace Contoso.Parameters.Expressions
{
    public class FirstOrDefaultOperatorParameter : FilterMethodOperatorParameterBase
    {
		public FirstOrDefaultOperatorParameter()
		{
		}

		public FirstOrDefaultOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public FirstOrDefaultOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}