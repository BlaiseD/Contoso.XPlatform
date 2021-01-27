namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class TrimOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
    }
}