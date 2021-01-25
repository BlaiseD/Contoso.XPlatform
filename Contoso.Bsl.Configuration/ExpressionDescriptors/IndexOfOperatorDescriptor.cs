namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class IndexOfOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
		public IExpressionDescriptor ItemToFind { get; set; }
    }
}