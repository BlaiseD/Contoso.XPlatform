namespace Contoso.Parameters.Expressions
{
    public class TotalSecondsOperatorParameter : IExpressionParameter
    {
		public TotalSecondsOperatorParameter()
		{
		}

		public TotalSecondsOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}