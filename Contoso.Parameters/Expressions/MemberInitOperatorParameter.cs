using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class MemberInitOperatorParameter : IExpressionParameter
    {
		public MemberInitOperatorParameter()
		{
		}

		public MemberInitOperatorParameter(IDictionary<string, IExpressionParameter> memberBindings, Type newType)
		{
			MemberBindings = memberBindings;
			NewType = newType;
		}

		public MemberInitOperatorParameter(IDictionary<string, IExpressionParameter> memberBindings)
		{
			MemberBindings = memberBindings;
		}

		public IDictionary<string, IExpressionParameter> MemberBindings { get; set; }
		public Type NewType { get; set; }
    }
}