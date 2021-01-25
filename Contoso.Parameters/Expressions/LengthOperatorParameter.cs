namespace Contoso.Parameters.Expressions
{
    public class LengthOperatorParameter : IExpressionParameter
    {
		public LengthOperatorParameter()
		{
		}

		public LengthOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}