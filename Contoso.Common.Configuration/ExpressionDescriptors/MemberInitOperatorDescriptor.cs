using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class MemberInitOperatorDescriptor : OperatorDescriptorBase
    {
		public IDictionary<string, OperatorDescriptorBase> MemberBindings { get; set; }
		public string NewType { get; set; }
    }
}