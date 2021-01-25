namespace Contoso.Parameters.Expressions
{
    public class MinuteOperatorParameter : IExpressionParameter
    {
		public MinuteOperatorParameter()
		{
		}

		public MinuteOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}