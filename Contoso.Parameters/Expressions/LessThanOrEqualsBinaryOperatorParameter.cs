namespace Contoso.Parameters.Expressions
{
    public class LessThanOrEqualsBinaryOperatorParameter : BinaryOperatorParameter
    {
		public LessThanOrEqualsBinaryOperatorParameter()
		{
		}

		public LessThanOrEqualsBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}