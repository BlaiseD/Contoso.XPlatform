namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToStringOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}