namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class IndexOfOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public IExpressionOperatorDescriptor ItemToFind { get; set; }
    }
}