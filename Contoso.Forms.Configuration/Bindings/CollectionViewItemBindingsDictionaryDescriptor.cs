using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Bindings
{
    public class CollectionViewItemBindingsDictionaryDescriptor : Dictionary<string, CollectionViewItemBindingDescriptor>
    {
        private List<CollectionViewItemBindingDescriptor> collectionViewItemBindingDescriptors;

        public CollectionViewItemBindingsDictionaryDescriptor(List<CollectionViewItemBindingDescriptor> collectionViewItemBindingDescriptors)
            => this.CollectionViewItemBindingDescriptors = collectionViewItemBindingDescriptors;

        public CollectionViewItemBindingsDictionaryDescriptor()
        {
        }

        public List<CollectionViewItemBindingDescriptor> CollectionViewItemBindingDescriptors
        {
            get => collectionViewItemBindingDescriptors;
            set
            {
                collectionViewItemBindingDescriptors = value;
                this.Clear();
                collectionViewItemBindingDescriptors.ForEach(ibd => this.Add(ibd.Name, ibd));
            }
        }
    }
}
