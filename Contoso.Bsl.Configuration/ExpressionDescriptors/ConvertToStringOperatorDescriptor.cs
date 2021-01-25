namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToStringOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}