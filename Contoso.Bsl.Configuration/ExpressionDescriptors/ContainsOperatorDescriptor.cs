namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ContainsOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}