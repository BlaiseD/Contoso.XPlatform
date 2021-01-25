namespace Contoso.Parameters.Expressions
{
    public class IndexOfOperatorParameter : IExpressionParameter
    {
		public IndexOfOperatorParameter()
		{
		}

		public IndexOfOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter itemToFind)
		{
			SourceOperand = sourceOperand;
			ItemToFind = itemToFind;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter ItemToFind { get; set; }
    }
}