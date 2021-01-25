using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class FilterLambdaOperatorDescriptor : IExpressionDescriptor
    {
		public IExpressionDescriptor FilterBody { get; set; }
		public string SourceElementType { get; set; }
		public string ParameterName { get; set; }
    }
}