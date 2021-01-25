namespace Contoso.Parameters.Expressions
{
    public class FirstOrDefaultOperatorParameter : FilterMethodOperatorParameterBase
    {
		public FirstOrDefaultOperatorParameter()
		{
		}

		public FirstOrDefaultOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}