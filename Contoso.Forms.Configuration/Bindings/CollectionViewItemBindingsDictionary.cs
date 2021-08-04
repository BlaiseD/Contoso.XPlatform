using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Bindings
{
    public class CollectionViewItemBindingsDictionary : Dictionary<string, CollectionViewItemBindingDescriptor>
    {
        private List<CollectionViewItemBindingDescriptor> collectionViewItemBindingDescriptors;

        public CollectionViewItemBindingsDictionary(List<CollectionViewItemBindingDescriptor> collectionViewItemBindingDescriptors)
            => this.CollectionViewItemBindingDescriptors = collectionViewItemBindingDescriptors;

        public CollectionViewItemBindingsDictionary()
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
