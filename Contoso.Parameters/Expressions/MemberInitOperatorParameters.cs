using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class MemberInitOperatorParameters : IExpressionParameter
    {
		public MemberInitOperatorParameters()
		{
		}

		public MemberInitOperatorParameters(IDictionary<string, IExpressionParameter> memberBindings, Type newType = null)
		{
			MemberBindings = memberBindings;
			NewType = newType;
		}

		public IDictionary<string, IExpressionParameter> MemberBindings { get; set; }
		public Type NewType { get; set; }
    }
}