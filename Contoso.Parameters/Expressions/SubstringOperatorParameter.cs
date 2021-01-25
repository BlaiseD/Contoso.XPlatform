namespace Contoso.Parameters.Expressions
{
    public class SubstringOperatorParameter : IExpressionParameter
    {
		public SubstringOperatorParameter()
		{
		}

		public SubstringOperatorParameter(IExpressionParameter sourceOperand, params IExpressionParameter[] indexes)
		{
			SourceOperand = sourceOperand;
			Indexes = indexes;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter[] Indexes { get; set; }
    }
}