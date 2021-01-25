namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToUpperOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}