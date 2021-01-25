using System.Collections.Generic;

namespace Contoso.Parameters.Expressions
{
    abstract public class SelectorMethodOperatorParameterBase : IExpressionParameter
    {
		public SelectorMethodOperatorParameterBase()
		{
		}

		public SelectorMethodOperatorParameterBase(IExpressionParameter sourceOperand, IExpressionParameter selectorBody = null, string selectorParameterName = null)
		{
			SourceOperand = sourceOperand;
			SelectorBody = selectorBody;
			SelectorParameterName = selectorParameterName;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter SelectorBody { get; set; }
		public string SelectorParameterName { get; set; }
    }
}