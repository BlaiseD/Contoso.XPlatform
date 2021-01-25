namespace Contoso.Parameters.Expressions
{
    public class AllOperatorParameter : FilterMethodOperatorParameterBase
    {
		public AllOperatorParameter()
		{
		}

		public AllOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public AllOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}