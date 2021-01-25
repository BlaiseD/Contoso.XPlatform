namespace Contoso.Parameters.Expressions
{
    public class LastOrDefaultOperatorParameter : FilterMethodOperatorParameterBase
    {
		public LastOrDefaultOperatorParameter()
		{
		}

		public LastOrDefaultOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}