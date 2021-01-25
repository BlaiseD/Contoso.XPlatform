namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TotalSecondsOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}