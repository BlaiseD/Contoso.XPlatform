namespace Contoso.Parameters.Expressions
{
    public class SkipOperatorParameter : IExpressionParameter
    {
		public SkipOperatorParameter()
		{
		}

		public SkipOperatorParameter(IExpressionParameter sourceOperand, int count)
		{
			SourceOperand = sourceOperand;
			Count = count;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public int Count { get; set; }
    }
}