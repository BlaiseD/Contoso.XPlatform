namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SkipOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
		public int Count { get; set; }
    }
}