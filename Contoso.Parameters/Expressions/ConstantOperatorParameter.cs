using System;

namespace Contoso.Parameters.Expressions
{
    public class ConstantOperatorParameter : IExpressionParameter
    {
		public ConstantOperatorParameter()
		{
		}

		public ConstantOperatorParameter(object constantValue, Type type = null)
		{
			ConstantValue = constantValue;
			Type = type;
		}

		public Type Type { get; set; }
		public object ConstantValue { get; set; }
    }
}