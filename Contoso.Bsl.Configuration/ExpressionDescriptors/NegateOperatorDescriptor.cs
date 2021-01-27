namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class NegateOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
    }
}