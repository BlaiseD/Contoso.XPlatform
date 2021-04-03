using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class IsOfOperatorDescriptor : OperatorDescriptorBase
    {
		public OperatorDescriptorBase Operand { get; set; }
		public string Type { get; set; }
    }
}