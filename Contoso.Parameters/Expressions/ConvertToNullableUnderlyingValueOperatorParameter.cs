namespace Contoso.Parameters.Expressions
{
    public class ConvertToNullableUnderlyingValueOperatorParameter : IExpressionParameter
    {
		public ConvertToNullableUnderlyingValueOperatorParameter()
		{
		}

		public ConvertToNullableUnderlyingValueOperatorParameter(IExpressionParameter sourceOperand)
		{
			SourceOperand = sourceOperand;
		}

		public IExpressionParameter SourceOperand { get; set; }
    }
}