using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MemberInitOperatorDescriptor : IExpressionDescriptor
    {
		public IDictionary<string, IExpressionDescriptor> MemberBindings { get; set; }
		public string NewType { get; set; }
    }
}