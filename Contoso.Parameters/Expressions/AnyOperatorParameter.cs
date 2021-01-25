namespace Contoso.Parameters.Expressions
{
    public class AnyOperatorParameter : FilterMethodOperatorParameterBase
    {
		public AnyOperatorParameter()
		{
		}

		public AnyOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}