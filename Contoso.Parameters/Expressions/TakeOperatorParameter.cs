namespace Contoso.Parameters.Expressions
{
    public class TakeOperatorParameter : IExpressionParameter
    {
		public TakeOperatorParameter()
		{
		}

		public TakeOperatorParameter(IExpressionParameter sourceOperand, int count)
		{
			SourceOperand = sourceOperand;
			Count = count;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public int Count { get; set; }
    }
}