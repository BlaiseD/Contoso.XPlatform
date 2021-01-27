namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class DistinctOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}