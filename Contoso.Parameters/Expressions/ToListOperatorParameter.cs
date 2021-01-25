namespace Contoso.Parameters.Expressions
{
    public class ToListOperatorParameter : IExpressionParameter
    {
		public ToListOperatorParameter()
		{
		}

		public ToListOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}