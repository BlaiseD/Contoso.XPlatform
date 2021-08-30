using Contoso.Forms.Configuration.ItemFilter.Json;
using System.Text.Json.Serialization;

namespace Contoso.Forms.Configuration.ItemFilter
{
    [JsonConverter(typeof(ItemFilterDescriptorConverter))]
    public abstract class ItemFilterDescriptorBase
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
