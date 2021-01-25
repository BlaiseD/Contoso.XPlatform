namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class BinaryOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Left { get; set; }
		public IExpressionDescriptor Right { get; set; }
    }
}