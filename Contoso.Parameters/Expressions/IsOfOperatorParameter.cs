using System;

namespace Contoso.Parameters.Expressions
{
    public class IsOfOperatorParameter : IExpressionParameter
    {
		public IsOfOperatorParameter()
		{
		}

		public IsOfOperatorParameter(IExpressionParameter operand, Type type)
		{
			Operand = operand;
			Type = type;
		}

		public IExpressionParameter Operand { get; set; }
		public Type Type { get; set; }
    }
}