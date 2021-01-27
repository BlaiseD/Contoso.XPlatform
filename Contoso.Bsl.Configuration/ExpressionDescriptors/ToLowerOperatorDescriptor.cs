namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToLowerOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
    }
}