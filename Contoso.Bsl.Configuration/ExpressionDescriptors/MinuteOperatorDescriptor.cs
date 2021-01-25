namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MinuteOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}