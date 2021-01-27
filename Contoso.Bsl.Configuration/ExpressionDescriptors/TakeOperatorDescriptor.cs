namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TakeOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public int Count { get; set; }
    }
}