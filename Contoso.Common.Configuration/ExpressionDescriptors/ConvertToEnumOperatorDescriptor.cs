using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertToEnumOperatorDescriptor : OperatorDescriptorBase
    {
		public string Type { get; set; }
		public object ConstantValue { get; set; }
    }
}