namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MonthOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}