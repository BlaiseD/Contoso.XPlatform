namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToNullableUnderlyingValueOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}