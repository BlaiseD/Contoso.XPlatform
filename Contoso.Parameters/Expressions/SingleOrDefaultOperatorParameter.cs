namespace Contoso.Parameters.Expressions
{
    public class SingleOrDefaultOperatorParameter : FilterMethodOperatorParameterBase
    {
		public SingleOrDefaultOperatorParameter()
		{
		}

		public SingleOrDefaultOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public SingleOrDefaultOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}