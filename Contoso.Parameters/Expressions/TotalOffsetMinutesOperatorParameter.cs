namespace Contoso.Parameters.Expressions
{
    public class TotalOffsetMinutesOperatorParameter : IExpressionParameter
    {
		public TotalOffsetMinutesOperatorParameter()
		{
		}

		public TotalOffsetMinutesOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}