using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class ThenByOperatorDescriptor : SelectorMethodOperatorDescriptorBase
    {
		public ListSortDirection SortDirection { get; set; }
    }
}