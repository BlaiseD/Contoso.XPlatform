using System.Collections.Generic;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class SelectorMethodOperatorDescriptorBase : IExpressionDescriptor
    {
		public IExpressionDescriptor SourceOperand { get; set; }
		public IExpressionDescriptor SelectorBody { get; set; }
		public string SelectorParameterName { get; set; }
    }
}