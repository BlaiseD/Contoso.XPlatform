namespace Contoso.Parameters.Expressions
{
    public class HourOperatorParameter : IExpressionParameter
    {
		public HourOperatorParameter()
		{
		}

		public HourOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}