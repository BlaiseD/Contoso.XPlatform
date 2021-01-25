namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class DistinctOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}