namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class HasOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}