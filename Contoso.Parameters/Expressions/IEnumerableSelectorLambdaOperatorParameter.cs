﻿using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class IEnumerableSelectorLambdaOperatorParameter : IExpressionParameter
    {
		public IEnumerableSelectorLambdaOperatorParameter()
		{
		}

		public IEnumerableSelectorLambdaOperatorParameter(IExpressionParameter selector, Type sourceElementType, string parameterName)
		{
			Selector = selector;
			SourceElementType = sourceElementType;
			ParameterName = parameterName;
		}

		public IExpressionParameter Selector { get; set; }
		public Type SourceElementType { get; set; }
		public string ParameterName { get; set; }
    }
}