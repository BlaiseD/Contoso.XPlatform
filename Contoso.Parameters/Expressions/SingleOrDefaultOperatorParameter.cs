namespace Contoso.Parameters.Expressions
{
    public class SingleOrDefaultOperatorParameter : FilterMethodOperatorParameterBase
    {
		public SingleOrDefaultOperatorParameter()
		{
		}

		public SingleOrDefaultOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null) : base(sourceOperand, filterBody, filterParameterName)
		{
		}
    }
}