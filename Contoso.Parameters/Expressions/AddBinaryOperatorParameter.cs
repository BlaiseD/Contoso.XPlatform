namespace Contoso.Parameters.Expressions
{
    public class AddBinaryOperatorParameter : BinaryOperatorParameter
    {
		public AddBinaryOperatorParameter()
		{
		}

		public AddBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}