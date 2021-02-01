namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SubstringOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public IExpressionOperatorDescriptor[] Indexes { get; set; }
    }
}