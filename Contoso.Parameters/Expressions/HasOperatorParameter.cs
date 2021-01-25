namespace Contoso.Parameters.Expressions
{
    public class HasOperatorParameter : IExpressionParameter
    {
		public HasOperatorParameter()
		{
		}

		public HasOperatorParameter(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}