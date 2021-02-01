using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class IEnumerableSelectorLambdaOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor Selector { get; set; }
		public string SourceElementType { get; set; }
		public string ParameterName { get; set; }
    }
}