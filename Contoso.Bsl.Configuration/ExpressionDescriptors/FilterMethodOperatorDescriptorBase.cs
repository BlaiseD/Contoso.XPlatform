using System.Collections.Generic;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class FilterMethodOperatorDescriptorBase : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
		public IExpressionDescriptor FilterBody { get; set; }
		public string FilterParameterName { get; set; }
    }
}