namespace Contoso.Parameters.Expressions
{
    public class WhereOperatorParameter : FilterMethodOperatorParameterBase
    {
		public WhereOperatorParameter()
		{
		}

		public WhereOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}