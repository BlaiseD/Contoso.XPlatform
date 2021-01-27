namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToListOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}