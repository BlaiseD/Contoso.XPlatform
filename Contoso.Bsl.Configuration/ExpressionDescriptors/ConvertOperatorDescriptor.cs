using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertOperatorDescriptor : IExpressionDescriptor
    {
		public string Type { get; set; }
		public IExpressionDescriptor SourceOperand { get; set; }
    }
}