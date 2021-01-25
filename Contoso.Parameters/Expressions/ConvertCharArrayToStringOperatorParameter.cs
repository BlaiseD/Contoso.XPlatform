namespace Contoso.Parameters.Expressions
{
    public class ConvertCharArrayToStringOperatorParameter : IExpressionParameter
    {
		public ConvertCharArrayToStringOperatorParameter()
		{
		}

		public ConvertCharArrayToStringOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}