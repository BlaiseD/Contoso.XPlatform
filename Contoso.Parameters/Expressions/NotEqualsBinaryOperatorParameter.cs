namespace Contoso.Parameters.Expressions
{
    public class NotEqualsBinaryOperatorParameter : BinaryOperatorParameter
    {
		public NotEqualsBinaryOperatorParameter()
		{
		}

		public NotEqualsBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}