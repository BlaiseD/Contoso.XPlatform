namespace Contoso.Parameters.Expressions
{
    public class LessThanBinaryOperatorParameter : BinaryOperatorParameter
    {
		public LessThanBinaryOperatorParameter()
		{
		}

		public LessThanBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}