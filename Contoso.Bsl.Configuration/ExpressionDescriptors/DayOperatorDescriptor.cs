namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class DayOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}