﻿using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CollectionCastOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IExpressionOperatorDescriptor Operand { get; set; }
		public string Type { get; set; }
    }
}