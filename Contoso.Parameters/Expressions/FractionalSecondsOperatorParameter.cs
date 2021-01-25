namespace Contoso.Parameters.Expressions
{
    public class FractionalSecondsOperatorParameter : IExpressionParameter
    {
		public FractionalSecondsOperatorParameter()
		{
		}

		public FractionalSecondsOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}