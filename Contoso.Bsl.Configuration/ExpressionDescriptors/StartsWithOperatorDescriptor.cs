namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class StartsWithOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}