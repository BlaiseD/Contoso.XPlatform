namespace Contoso.Parameters.Expressions
{
    public class AllOperatorParameter : FilterMethodOperatorParameterBase
    {
		public AllOperatorParameter()
		{
		}

		public AllOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}