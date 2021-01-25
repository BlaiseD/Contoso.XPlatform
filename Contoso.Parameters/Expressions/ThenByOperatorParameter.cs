using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Parameters.Expressions
{
    public class ThenByOperatorParameter : SelectorMethodOperatorParameterBase
    {
		public ThenByOperatorParameter()
		{
		}

		public ThenByOperatorParameter(IExpressionParameter sourceOperand, IExpressionParameter selectorBody, ListSortDirection sortDirection, string selectorParameterName) : base(sourceOperand, selectorBody, selectorParameterName)
		{
			SortDirection = sortDirection;
		}

		public ListSortDirection SortDirection { get; set; }
    }
}