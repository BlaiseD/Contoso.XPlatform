﻿using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ConstantOperatorDescriptor : IExpressionDescriptor
    {
		public string Type { get; set; }
		public object ConstantValue { get; set; }
    }
}