namespace Contoso.Parameters.Expressions
{
    public class TimeOperatorParameter : IExpressionParameter
    {
		public TimeOperatorParameter()
		{
		}

		public TimeOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}