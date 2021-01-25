namespace Contoso.Parameters.Expressions
{
    public class DateOperatorParameter : IExpressionParameter
    {
		public DateOperatorParameter()
		{
		}

		public DateOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}