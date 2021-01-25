namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToNumericDateOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}