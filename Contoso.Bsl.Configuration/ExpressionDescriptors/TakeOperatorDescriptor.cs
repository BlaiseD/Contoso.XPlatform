namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TakeOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
		public int Count { get; set; }
    }
}