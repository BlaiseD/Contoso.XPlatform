namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TrimOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}