namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToNullableUnderlyingValueOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}