namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ToUpperOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
    }
}