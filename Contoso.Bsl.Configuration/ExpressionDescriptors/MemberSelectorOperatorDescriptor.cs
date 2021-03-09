namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MemberSelectorOperatorDescriptor : OperatorDescriptorBase
    {
		public string MemberFullName { get; set; }
		public OperatorDescriptorBase SourceOperand { get; set; }
    }
}