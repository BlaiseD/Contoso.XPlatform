namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class NotOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
    }
}