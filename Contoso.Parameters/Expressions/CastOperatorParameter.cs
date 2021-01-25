using System;

namespace Contoso.Parameters.Expressions
{
    public class CastOperatorParameter : IExpressionParameter
    {
		public CastOperatorParameter()
		{
		}

		public CastOperatorParameter(IExpressionParameter operand, Type type)
		{
			Operand = operand;
			Type = type;
		}

		public IExpressionParameter Operand { get; set; }
		public Type Type { get; set; }
    }
}