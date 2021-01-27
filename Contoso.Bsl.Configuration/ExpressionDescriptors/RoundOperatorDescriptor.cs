namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class RoundOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
    }
}