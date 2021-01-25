using System.Collections.Generic;

namespace Contoso.Parameters.Expressions
{
    abstract public class FilterMethodOperatorParameterBase : IExpressionParameter
    {
		public FilterMethodOperatorParameterBase()
		{
		}

		public FilterMethodOperatorParameterBase(IExpressionParameter sourceOperand, IExpressionParameter filterBody = null, string filterParameterName = null)
		{
			SourceOperand = sourceOperand;
			FilterBody = filterBody;
			FilterParameterName = filterParameterName;
		}

		public IExpressionParameter SourceOperand { get; set; }
		public IExpressionParameter FilterBody { get; set; }
		public string FilterParameterName { get; set; }
    }
}