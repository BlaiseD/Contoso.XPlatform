namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToNumericTimeOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}