using System;

namespace Contoso.Parameters.Expressions
{
    public class ConvertToEnumOperatorParameter : IExpressionParameter
    {
		public ConvertToEnumOperatorParameter()
		{
		}

		public ConvertToEnumOperatorParameter(object constantValue, Type type)
		{
			ConstantValue = constantValue;
			Type = type;
		}

		public Type Type { get; set; }
		public object ConstantValue { get; set; }
    }
}