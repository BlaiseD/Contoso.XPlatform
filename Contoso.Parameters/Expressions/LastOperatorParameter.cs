namespace Contoso.Parameters.Expressions
{
    public class LastOperatorParameter : FilterMethodOperatorParameterBase
    {
		public LastOperatorParameter()
		{
		}

		public LastOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public LastOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}