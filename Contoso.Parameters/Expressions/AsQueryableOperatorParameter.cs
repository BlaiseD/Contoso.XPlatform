namespace Contoso.Parameters.Expressions
{
    public class AsQueryableOperatorParameter : IExpressionParameter
    {
		public AsQueryableOperatorParameter()
		{
		}

		public AsQueryableOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}