namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class HasOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}