using System;

namespace Contoso.Parameters.Expressions
{
    public class ConvertOperatorParameter : IExpressionParameter
    {
		public ConvertOperatorParameter()
		{
		}

		public ConvertOperatorParameter(IExpressionParameter sourceOperand, Type type)
		{
			SourceOperand = sourceOperand;
			Type = type;
		}

		public Type Type { get; set; }
		public IExpressionParameter SourceOperand { get; set; }
    }
}