namespace Contoso.Parameters.Expressions
{
    public class SingleOperatorParameter : FilterMethodOperatorParameterBase
    {
		public SingleOperatorParameter()
		{
		}

		public SingleOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter filterBody, string filterParameterName) : base(sourceOperand, filterBody, filterParameterName)
		{
		}

		public SingleOperatorParameter(IExpressionParameter sourceOperand) : base(sourceOperand)
		{
		}
    }
}