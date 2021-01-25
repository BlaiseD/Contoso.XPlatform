namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class EndsWithOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Left { get; set; }
		public IExpressionDescriptor Right { get; set; }
    }
}