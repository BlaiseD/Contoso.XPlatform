namespace Contoso.Parameters.Expressions
{
    public class GreaterThanOrEqualsBinaryOperatorParameter : BinaryOperatorParameter
    {
		public GreaterThanOrEqualsBinaryOperatorParameter()
		{
		}

		public GreaterThanOrEqualsBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}