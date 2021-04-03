namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class HasOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Left { get; set; }
		public OperatorDescriptorBase Right { get; set; }
    }
}