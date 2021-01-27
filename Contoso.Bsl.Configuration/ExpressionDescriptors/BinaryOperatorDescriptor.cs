namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class BinaryOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}