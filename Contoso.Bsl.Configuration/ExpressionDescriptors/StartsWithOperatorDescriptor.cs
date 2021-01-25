namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class StartsWithOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Left { get; set; }
		public IExpressionDescriptor Right { get; set; }
    }
}