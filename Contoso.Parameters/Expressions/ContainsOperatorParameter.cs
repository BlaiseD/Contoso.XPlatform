namespace Contoso.Parameters.Expressions
{
    public class ContainsOperatorParameter : IExpressionParameter
    {
		public ContainsOperatorParameter()
		{
		}

		public ContainsOperatorParameter(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}