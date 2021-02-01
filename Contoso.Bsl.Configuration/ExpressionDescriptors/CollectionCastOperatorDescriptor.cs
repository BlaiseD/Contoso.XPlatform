using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CollectionCastOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}