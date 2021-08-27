using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Bindings
{
    public class CollectionViewItemBindingsDictionaryParameters : Dictionary<string, CollectionViewItemBindingParameters>
    {
        private List<CollectionViewItemBindingParameters> collectionViewItemBindings;

        public CollectionViewItemBindingsDictionaryParameters(List<CollectionViewItemBindingParameters> collectionViewItemBindings)
        {
            CollectionViewItemBindings = collectionViewItemBindings;
        }

        public List<CollectionViewItemBindingParameters> CollectionViewItemBindings
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