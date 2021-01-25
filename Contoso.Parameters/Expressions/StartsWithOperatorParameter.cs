namespace Contoso.Parameters.Expressions
{
    public class StartsWithOperatorParameter : IExpressionParameter
    {
		public StartsWithOperatorParameter()
		{
		}

		public StartsWithOperatorParameter(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}