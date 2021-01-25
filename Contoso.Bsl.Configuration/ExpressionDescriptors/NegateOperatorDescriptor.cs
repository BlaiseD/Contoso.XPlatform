namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class NegateOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}