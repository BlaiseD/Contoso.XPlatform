namespace Contoso.Parameters.Expressions
{
    public class SubtractBinaryOperatorParameter : BinaryOperatorParameter
    {
		public SubtractBinaryOperatorParameter()
		{
		}

		public SubtractBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}