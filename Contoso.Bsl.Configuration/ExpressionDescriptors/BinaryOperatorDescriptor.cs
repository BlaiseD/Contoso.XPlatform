namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class BinaryOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}