using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CastOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}