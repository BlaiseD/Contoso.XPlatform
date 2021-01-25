namespace Contoso.Parameters.Expressions
{
    abstract public class BinaryOperatorParameter : IExpressionParameter
    {
		public BinaryOperatorParameter()
		{
		}

		public BinaryOperatorParameter(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}