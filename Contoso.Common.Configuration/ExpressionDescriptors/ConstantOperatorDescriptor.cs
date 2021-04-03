using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConstantOperatorDescriptor : OperatorDescriptorBase
    {
		public string Type { get; set; }
		public object ConstantValue { get; set; }
    }
}