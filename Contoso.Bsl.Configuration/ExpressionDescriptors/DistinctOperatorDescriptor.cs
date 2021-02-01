namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class DistinctOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}