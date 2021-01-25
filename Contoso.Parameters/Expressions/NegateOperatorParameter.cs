namespace Contoso.Parameters.Expressions
{
    public class NegateOperatorParameter : IExpressionParameter
    {
		public NegateOperatorParameter()
		{
		}

		public NegateOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}