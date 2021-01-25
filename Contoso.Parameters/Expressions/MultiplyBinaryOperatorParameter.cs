namespace Contoso.Parameters.Expressions
{
    public class MultiplyBinaryOperatorParameter : BinaryOperatorParameter
    {
		public MultiplyBinaryOperatorParameter()
		{
		}

		public MultiplyBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}