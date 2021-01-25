namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class HourOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}