namespace Contoso.Parameters.Expressions
{
    public class YearOperatorParameter : IExpressionParameter
    {
		public YearOperatorParameter()
		{
		}

		public YearOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}