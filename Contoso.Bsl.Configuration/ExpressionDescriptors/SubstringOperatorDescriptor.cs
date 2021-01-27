namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SubstringOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public IExpressionOperatorDescriptor[] Indexes { get; set; }
    }
}