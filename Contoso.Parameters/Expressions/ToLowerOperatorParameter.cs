namespace Contoso.Parameters.Expressions
{
    public class ToLowerOperatorParameter : IExpressionParameter
    {
		public ToLowerOperatorParameter()
		{
		}

		public ToLowerOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}