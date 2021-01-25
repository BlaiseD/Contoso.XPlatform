namespace Contoso.Parameters.Expressions
{
    public class DistinctOperatorParameter : IExpressionParameter
    {
		public DistinctOperatorParameter()
		{
		}

		public DistinctOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}