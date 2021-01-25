namespace Contoso.Parameters.Expressions
{
    public class GreaterThanBinaryOperatorParameter : BinaryOperatorParameter
    {
		public GreaterThanBinaryOperatorParameter()
		{
		}

		public GreaterThanBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}