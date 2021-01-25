namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConcatOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Left { get; set; }
		public IExpressionDescriptor Right { get; set; }
    }
}