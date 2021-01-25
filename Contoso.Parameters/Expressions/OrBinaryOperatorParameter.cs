namespace Contoso.Parameters.Expressions
{
    public class OrBinaryOperatorParameter : BinaryOperatorParameter
    {
		public OrBinaryOperatorParameter()
		{
		}

		public OrBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}