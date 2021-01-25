using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CollectionCastOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}