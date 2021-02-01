namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SkipOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public int Count { get; set; }
    }
}