namespace Contoso.Parameters.Expressions
{
    public class ConvertToNumericTimeOperatorParameter : IExpressionParameter
    {
		public ConvertToNumericTimeOperatorParameter()
		{
		}

		public ConvertToNumericTimeOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}