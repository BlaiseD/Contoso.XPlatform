namespace Contoso.Parameters.Expressions
{
    public class EqualsBinaryOperatorParameter : BinaryOperatorParameter
    {
		public EqualsBinaryOperatorParameter()
		{
		}

		public EqualsBinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right) : base(left, right)
		{
		}
    }
}