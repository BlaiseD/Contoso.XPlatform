using System;

namespace Contoso.Parameters.Expressions
{
    public class ConstantOperatorParameter : IExpressionParameter
    {
		public ConstantOperatorParameter()
		{
		}

		public ConstantOperatorParameter(object constantValue, Type type)
		{
			ConstantValue = constantValue;
			Type = type;
		}

		public ConstantOperatorParameter(object constantValue)
		{
			ConstantValue = constantValue;
		}

		public Type Type { get; set; }
		public object ConstantValue { get; set; }
    }
}