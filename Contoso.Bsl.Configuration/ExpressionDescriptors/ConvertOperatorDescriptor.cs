using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConvertOperatorDescriptor : OperatorDescriptorBase
    {
		public string Type { get; set; }
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
    }
}