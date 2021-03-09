namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SkipOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase SourceOperand { get; set; }
		public int Count { get; set; }
    }
}