namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class DateOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}