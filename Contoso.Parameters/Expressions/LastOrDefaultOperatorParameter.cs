namespace Contoso.Parameters.Expressions
{
    public class LastOrDefaultOperatorParameter : FilterMethodOperatorParameterBase
    {
		public LastOrDefaultOperatorParameter()
		{
		}

		public LastOrDefaultOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public LastOrDefaultOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}