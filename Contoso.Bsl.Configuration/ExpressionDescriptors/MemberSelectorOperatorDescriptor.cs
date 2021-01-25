namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MemberSelectorOperatorDescriptor : IExpressionDescriptor
    {
		public string MemberFullName { get; set; }
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}