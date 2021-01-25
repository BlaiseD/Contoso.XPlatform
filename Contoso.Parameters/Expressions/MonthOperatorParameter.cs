namespace Contoso.Parameters.Expressions
{
    public class MonthOperatorParameter : IExpressionParameter
    {
		public MonthOperatorParameter()
		{
		}

		public MonthOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}