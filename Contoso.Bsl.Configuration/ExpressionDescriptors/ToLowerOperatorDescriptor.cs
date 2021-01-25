namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToLowerOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}