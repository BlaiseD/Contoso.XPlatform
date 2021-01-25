namespace Contoso.Parameters.Expressions
{
    public class NotOperatorParameter : IExpressionParameter
    {
		public NotOperatorParameter()
		{
		}

		public NotOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}