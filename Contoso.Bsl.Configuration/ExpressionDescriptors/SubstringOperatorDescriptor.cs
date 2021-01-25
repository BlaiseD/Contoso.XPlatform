namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SubstringOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
		public IExpressionDescriptor[] Indexes { get; set; }
    }
}