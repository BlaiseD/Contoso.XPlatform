namespace Contoso.Parameters.Expressions
{
    public class ConvertToNumericDateOperatorParameter : IExpressionParameter
    {
		public ConvertToNumericDateOperatorParameter()
		{
		}

		public ConvertToNumericDateOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}