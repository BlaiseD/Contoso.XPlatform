using System.Collections.Generic;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class SelectorMethodOperatorDescriptorBase : OperatorDescriptorBase
    {
		public OperatorDescriptorBase SourceOperand { get; set; }
		public OperatorDescriptorBase SelectorBody { get; set; }
		public string SelectorParameterName { get; set; }
    }
}