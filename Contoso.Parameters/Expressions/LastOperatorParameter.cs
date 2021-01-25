namespace Contoso.Parameters.Expressions
{
    public class LastOperatorParameter : FilterMethodOperatorParameterBase
    {
		public LastOperatorParameter()
		{
		}

		public LastOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}