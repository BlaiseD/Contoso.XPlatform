namespace Contoso.Parameters.Expressions
{
    public class ConvertToStringOperatorParameter : IExpressionParameter
    {
		public ConvertToStringOperatorParameter()
		{
		}

		public ConvertToStringOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}