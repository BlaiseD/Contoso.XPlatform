namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class EndsWithOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}