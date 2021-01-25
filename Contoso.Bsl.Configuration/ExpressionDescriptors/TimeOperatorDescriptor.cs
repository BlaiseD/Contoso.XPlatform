namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TimeOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}