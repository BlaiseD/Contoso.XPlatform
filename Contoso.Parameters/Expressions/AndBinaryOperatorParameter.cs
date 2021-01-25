namespace Contoso.Parameters.Expressions
{
    public class AndBinaryOperatorParameter : BinaryOperatorParameter
    {
		public AndBinaryOperatorParameter()
		{
		}

		public AndBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}