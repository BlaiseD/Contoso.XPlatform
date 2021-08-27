using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Bindings
{
    public class CollectionViewItemBindingsDictionaryDescriptor : Dictionary<string, CollectionViewItemBindingDescriptor>
    {
        private List<CollectionViewItemBindingDescriptor> collectionViewItemBindings;

        public CollectionViewItemBindingsDictionaryDescriptor(List<CollectionViewItemBindingDescriptor> collectionViewItemBindingDescriptors)
            => this.CollectionViewItemBindings = collectionViewItemBindingDescriptors;

        public CollectionViewItemBindingsDictionaryDescriptor()
        {
        }

        public List<CollectionViewItemBindingDescriptor> CollectionViewItemBindings
        {
            get => collectionViewItemBindings;
            set
            {
                collectionViewItemBindings = value;
                this.Clear();
                collectionViewItemBindings.ForEach(ibd => this.Add(ibd.Name, ibd));
            }
        }
    }
}
