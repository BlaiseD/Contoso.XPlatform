namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class RoundOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}