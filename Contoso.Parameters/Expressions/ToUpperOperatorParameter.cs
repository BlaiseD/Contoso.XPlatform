namespace Contoso.Parameters.Expressions
{
    public class ToUpperOperatorParameter : IExpressionParameter
    {
		public ToUpperOperatorParameter()
		{
		}

		public ToUpperOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}