namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class HasOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Left { get; set; }
		public IExpressionDescriptor Right { get; set; }
    }
}