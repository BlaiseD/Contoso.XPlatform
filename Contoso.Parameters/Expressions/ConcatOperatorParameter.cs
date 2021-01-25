namespace Contoso.Parameters.Expressions
{
    public class ConcatOperatorParameter : IExpressionParameter
    {
		public ConcatOperatorParameter()
		{
		}

		public ConcatOperatorParameter(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}