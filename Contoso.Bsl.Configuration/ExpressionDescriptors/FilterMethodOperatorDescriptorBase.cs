using System.Collections.Generic;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class FilterMethodOperatorDescriptorBase : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public IExpressionOperatorDescriptor FilterBody { get; set; }
		public string FilterParameterName { get; set; }
    }
}