namespace Contoso.Parameters.Expressions
{
    public class SingleOperatorParameter : FilterMethodOperatorParameterBase
    {
		public SingleOperatorParameter()
		{
		}

		public SingleOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}