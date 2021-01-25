namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class LengthOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}