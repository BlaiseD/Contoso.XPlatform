namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class NotOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}