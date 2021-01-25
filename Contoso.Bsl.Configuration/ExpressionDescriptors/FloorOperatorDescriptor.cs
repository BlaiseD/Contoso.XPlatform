namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class FloorOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
    }
}