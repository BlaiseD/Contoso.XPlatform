namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToListOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}