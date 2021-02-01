namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToListOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}