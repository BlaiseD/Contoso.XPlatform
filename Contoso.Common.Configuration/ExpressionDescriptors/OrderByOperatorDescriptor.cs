using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class OrderByOperatorDescriptor : SelectorMethodOperatorDescriptorBase
    {
		public ListSortDirection SortDirection { get; set; }
    }
}