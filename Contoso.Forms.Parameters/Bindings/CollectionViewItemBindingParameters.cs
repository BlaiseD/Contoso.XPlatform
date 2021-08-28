using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Bindings
{
    public class CollectionViewItemBindingParameters
    {
		public CollectionViewItemBindingParameters
		(
			[Comments("")]
			string name,

			[Comments("")]
			string property,

			[Comments("")]
			string stringFormat
		)
		{
			Name = name;
			Property = property;
			StringFormat = stringFormat;
		}

		public string Name { get; set; }
		public string Property { get; set; }
		public string StringFormat { get; set; }
    }
}