namespace Contoso.Parameters.Expressions
{
    public class AnyOperatorParameter : FilterMethodOperatorParameterBase
    {
		public AnyOperatorParameter()
		{
		}

		public AnyOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public AnyOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}