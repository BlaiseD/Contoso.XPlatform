﻿using System.Collections.Generic;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    abstract public class SelectorMethodOperatorDescriptorBase : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor SourceOperand { get; set; }
		public IExpressionOperatorDescriptor SelectorBody { get; set; }
		public string SelectorParameterName { get; set; }
    }
}