using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MemberInitOperatorDescriptor : IExpressionOperatorDescriptor
    {
		public IDictionary<string, IExpressionOperatorDescriptor> MemberBindings { get; set; }
		public string NewType { get; set; }
    }
}