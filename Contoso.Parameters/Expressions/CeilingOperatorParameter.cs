namespace Contoso.Parameters.Expressions
{
    public class CeilingOperatorParameter : IExpressionParameter
    {
		public CeilingOperatorParameter()
		{
		}

		public CeilingOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}