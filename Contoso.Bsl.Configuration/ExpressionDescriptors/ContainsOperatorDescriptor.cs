namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ContainsOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Left { get; set; }
		public IExpressionDescriptor Right { get; set; }
    }
}