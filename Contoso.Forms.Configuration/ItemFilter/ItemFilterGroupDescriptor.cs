using System.Collections.Generic;

namespace Contoso.Forms.Configuration.ItemFilter
{
    public class ItemFilterGroupDescriptor : ItemFilterDescriptorBase
    {
        public string Logic { get; set; }
        public ICollection<ItemFilterDescriptorBase> Filters { get; set; }
    }
}
