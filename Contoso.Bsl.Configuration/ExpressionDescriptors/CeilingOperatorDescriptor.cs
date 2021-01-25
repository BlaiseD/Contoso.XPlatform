namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CeilingOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}