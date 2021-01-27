﻿using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class SelectorLambdaOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Selector { get; set; }
		public string SourceElementType { get; set; }
		public string BodyType { get; set; }
		public string ParameterName { get; set; }
    }
}