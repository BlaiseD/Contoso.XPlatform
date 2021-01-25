namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SecondOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}