using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CastOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}