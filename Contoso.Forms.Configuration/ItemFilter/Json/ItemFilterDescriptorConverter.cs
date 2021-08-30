using Contoso.Utils;

namespace Contoso.Forms.Configuration.ItemFilter.Json
{
    public class ItemFilterDescriptorConverter : JsonTypeConverter<ItemFilterDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
