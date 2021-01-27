namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class IndexOfOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public IExpressionOperatorDescriptor ItemToFind { get; set; }
    }
}