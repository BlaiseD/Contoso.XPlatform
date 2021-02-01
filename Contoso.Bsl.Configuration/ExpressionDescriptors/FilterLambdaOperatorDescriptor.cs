using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class FilterLambdaOperatorDescriptor : OperatorDescriptorBase
    {
		public IExpressionOperatorDescriptor FilterBody { get; set; }
		public string SourceElementType { get; set; }
		public string ParameterName { get; set; }
    }
}