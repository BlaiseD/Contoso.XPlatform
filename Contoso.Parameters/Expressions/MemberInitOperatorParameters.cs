using System.Collections.Generic;
using System;
using System.Linq;

namespace Contoso.Parameters.Expressions
{
    public class MemberInitOperatorParameters : IExpressionParameter
    {
		public MemberInitOperatorParameters()
		{
		}

		public MemberInitOperatorParameters(IList<MemberBindingItem> memberBindings, Type newType = null)
		{
			MemberBindings = memberBindings.ToDictionary(m => m.Property, m => m.Selector);
			NewType = newType;
		}

		public IDictionary<string, IExpressionParameter> MemberBindings { get; set; }
		public Type NewType { get; set; }
    }
}