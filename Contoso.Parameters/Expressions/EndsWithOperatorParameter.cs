namespace Contoso.Parameters.Expressions
{
    public class EndsWithOperatorParameter : IExpressionParameter
    {
		public EndsWithOperatorParameter()
		{
		}

		public EndsWithOperatorParameter(IExpressionParameter left, IExpressionParameter right)
		{
			Left = left;
			Right = right;
		}

		public IExpressionParameter Left { get; set; }
		public IExpressionParameter Right { get; set; }
    }
}