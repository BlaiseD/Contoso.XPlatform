using System;

namespace Contoso.Parameters.Expressions
{
    public class CollectionCastOperatorParameter : IExpressionParameter
    {
		public CollectionCastOperatorParameter()
		{
		}

		public CollectionCastOperatorParameter(IExpressionParameter operand, Type type)
		{
			Operand = operand;
			Type = type;
		}

		public IExpressionParameter Operand { get; set; }
		public Type Type { get; set; }
    }
}