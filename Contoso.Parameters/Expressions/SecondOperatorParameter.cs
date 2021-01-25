namespace Contoso.Parameters.Expressions
{
    public class SecondOperatorParameter : IExpressionParameter
    {
		public SecondOperatorParameter()
		{
		}

		public SecondOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}