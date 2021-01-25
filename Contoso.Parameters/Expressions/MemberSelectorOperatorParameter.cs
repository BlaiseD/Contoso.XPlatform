namespace Contoso.Parameters.Expressions
{
    public class MemberSelectorOperatorParameter : IExpressionParameter
    {
		public MemberSelectorOperatorParameter()
		{
		}

		public MemberSelectorOperatorParameter(string memberFullName, IExpressionParameter sourceOperand)
		{
			MemberFullName = memberFullName;
			SourceOperand = sourceOperand;
		}

		public string MemberFullName { get; set; }
		public IExpressionParameter SourceOperand { get; set; }
    }
}