using System.Collections.Generic;

namespace Contoso.Parameters.Expressions
{
    public class ParameterOperatorParameter : IExpressionParameter
    {
		public ParameterOperatorParameter()
		{
		}

		public ParameterOperatorParameter(string parameterName)
		{
			ParameterName = parameterName;
		}

		public string ParameterName { get; set; }
    }
}