using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CastOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}