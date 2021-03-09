namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class StartsWithOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Left { get; set; }
		public OperatorDescriptorBase Right { get; set; }
    }
}