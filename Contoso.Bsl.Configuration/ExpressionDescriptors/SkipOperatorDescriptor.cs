namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SkipOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public int Count { get; set; }
    }
}