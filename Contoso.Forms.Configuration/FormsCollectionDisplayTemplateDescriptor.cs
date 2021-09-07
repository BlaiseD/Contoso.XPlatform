using Contoso.Forms.Configuration.Bindings;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration
{
    public class FormsCollectionDisplayTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public Dictionary<string, CollectionViewItemBindingDescriptor> Bindings { get; set; }
    }
}
