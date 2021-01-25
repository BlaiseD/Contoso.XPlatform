namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class AsQueryableOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}