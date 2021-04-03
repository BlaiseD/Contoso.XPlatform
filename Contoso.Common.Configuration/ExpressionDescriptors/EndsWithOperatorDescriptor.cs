namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class EndsWithOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Left { get; set; }
		public OperatorDescriptorBase Right { get; set; }
    }
}