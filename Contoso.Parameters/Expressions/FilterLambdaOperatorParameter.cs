using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class FilterLambdaOperatorParameter : IExpressionParameter
    {
		public FilterLambdaOperatorParameter()
		{
		}

		public FilterLambdaOperatorParameter(IExpressionParameter filterBody, Type sourceElementType, string parameterName)
		{
			FilterBody = filterBody;
			SourceElementType = sourceElementType;
			ParameterName = parameterName;
		}

		public IExpressionParameter FilterBody { get; set; }
		public Type SourceElementType { get; set; }
		public string ParameterName { get; set; }
    }
}