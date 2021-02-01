namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConcatOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Left { get; set; }
		public IExpressionOperatorDescriptor Right { get; set; }
    }
}