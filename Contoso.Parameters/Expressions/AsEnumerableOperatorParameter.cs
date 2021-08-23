namespace Contoso.Parameters.Expressions
{
    public class AsEnumerableOperatorParameter : IExpressionParameter
    {
		public AsEnumerableOperatorParameter()
		{
		}

		public AsEnumerableOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}