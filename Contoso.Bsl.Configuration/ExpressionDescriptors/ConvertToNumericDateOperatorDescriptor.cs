namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToNumericDateOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}