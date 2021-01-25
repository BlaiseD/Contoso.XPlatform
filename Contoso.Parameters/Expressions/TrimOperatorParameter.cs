namespace Contoso.Parameters.Expressions
{
    public class TrimOperatorParameter : IExpressionParameter
    {
		public TrimOperatorParameter()
		{
		}

		public TrimOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}