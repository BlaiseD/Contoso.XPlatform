namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class EndsWithOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}