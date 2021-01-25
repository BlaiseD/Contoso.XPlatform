namespace Contoso.Parameters.Expressions
{
    public class RoundOperatorParameter : IExpressionParameter
    {
		public RoundOperatorParameter()
		{
		}

		public RoundOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}