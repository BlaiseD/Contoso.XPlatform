namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class AsQueryableOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}