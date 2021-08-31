using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration.Bindings;

namespace Contoso.Forms.Configuration
{
    public class FormsCollectionDisplayTemplateDescriptor
    {
        public string TemplateName { get; set; }
        public CollectionViewItemBindingsDictionaryDescriptor Bindings { get; set; }
    }
}
