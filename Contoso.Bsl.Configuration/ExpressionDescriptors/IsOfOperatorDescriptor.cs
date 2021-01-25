using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class IsOfOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}