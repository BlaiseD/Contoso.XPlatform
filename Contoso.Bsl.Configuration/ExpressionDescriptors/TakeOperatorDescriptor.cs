namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TakeOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public int Count { get; set; }
    }
}