using LogicBuilder.Expressions.Utils.Strutures;

namespace Contoso.Parameters.Expansions
{
    public class SortDescriptionParameters
    {
        public SortDescriptionParameters()
        {

        }

        public SortDescriptionParameters(string propertyName, ListSortDirection order)
        {
            this.PropertyName = propertyName;
            this.SortDirection = order;
        }

        public string PropertyName { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
