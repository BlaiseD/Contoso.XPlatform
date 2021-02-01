using System.Collections.Generic;

namespace Contoso.Parameters.Expansions
{
    public class SortCollectionParameters
    {
        public ICollection<SortDescriptionParameters> SortDescriptions { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
