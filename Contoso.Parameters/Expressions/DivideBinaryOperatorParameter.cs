namespace Contoso.Parameters.Expressions
{
    public class DivideBinaryOperatorParameter : BinaryOperatorParameter
    {
		public DivideBinaryOperatorParameter()
		{
		}

		public DivideBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}