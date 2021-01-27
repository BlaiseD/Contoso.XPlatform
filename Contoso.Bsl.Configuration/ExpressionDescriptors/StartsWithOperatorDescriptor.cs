namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class StartsWithOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}