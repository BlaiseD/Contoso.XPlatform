namespace Contoso.Parameters.Expressions
{
    public class FloorOperatorParameter : IExpressionParameter
    {
		public FloorOperatorParameter()
		{
		}

		public FloorOperatorParameter(IExpressionParameter operand)
		{
			Operand = operand;
		}

		public IExpressionParameter Operand { get; set; }
    }
}