namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConcatOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}