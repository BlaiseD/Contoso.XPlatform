namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToNumericTimeOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}