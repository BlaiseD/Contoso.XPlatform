using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Bindings
{
    public class CollectionViewItemBindingsDictionaryParameters : Dictionary<string, CollectionViewItemBindingParameters>
    {
		private List<CollectionViewItemBindingParameters> collectionViewItemBindings;

		public CollectionViewItemBindingsDictionaryParameters
		(
			[Comments("List of bindings for a collection view item item template.")]
			List<CollectionViewItemBindingParameters> collectionViewItemBindings
		)
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