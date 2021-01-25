using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class SelectorLambdaOperatorParameter : IExpressionParameter
    {
		public SelectorLambdaOperatorParameter()
		{
		}

		public SelectorLambdaOperatorParameter(IExpressionParameter selector, Type sourceElementType, string parameterName)
		{
			Selector = selector;
			SourceElementType = sourceElementType;
			ParameterName = parameterName;
		}

		public SelectorLambdaOperatorParameter(IExpressionParameter selector, Type sourceElementType, Type bodyType, string parameterName)
		{
			Selector = selector;
			SourceElementType = sourceElementType;
			BodyType = bodyType;
			ParameterName = parameterName;
		}

		public IExpressionParameter Selector { get; set; }
		public Type SourceElementType { get; set; }
		public Type BodyType { get; set; }
		public string ParameterName { get; set; }
    }
}