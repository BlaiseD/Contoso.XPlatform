using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public string Type { get; set; }
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}