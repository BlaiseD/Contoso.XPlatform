namespace Contoso.Parameters.Expressions
{
    public class DayOperatorParameter : IExpressionParameter
    {
		public DayOperatorParameter()
		{
		}

		public DayOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}