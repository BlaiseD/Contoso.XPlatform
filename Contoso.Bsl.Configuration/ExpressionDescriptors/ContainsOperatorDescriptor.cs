namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ContainsOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}