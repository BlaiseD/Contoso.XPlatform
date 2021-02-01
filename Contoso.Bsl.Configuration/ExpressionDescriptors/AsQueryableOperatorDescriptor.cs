namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class AsQueryableOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}