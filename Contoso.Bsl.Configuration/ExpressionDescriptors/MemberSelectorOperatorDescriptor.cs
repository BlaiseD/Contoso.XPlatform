namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MemberSelectorOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public string MemberFullName { get; set; }
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}