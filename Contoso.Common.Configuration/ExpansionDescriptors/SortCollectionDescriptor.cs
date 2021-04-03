using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Bsl.Configuration.ExpansionDescriptors
{
    public class SortCollectionDescriptor
    {
        public ICollection<SortDescriptionDescriptor> SortDescriptions { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
