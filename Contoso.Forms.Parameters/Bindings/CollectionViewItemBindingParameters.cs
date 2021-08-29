using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Bindings
{
    public class CollectionViewItemBindingParameters
    {
		public CollectionViewItemBindingParameters
		(
			[Comments("The section of the item template we're binding the property to.")]
			[Domain("Header,Text,Detail")]
			[NameValue(AttributeNames.USEFOREQUALITY, "true")]
			[NameValue(AttributeNames.USEFORHASHCODE, "true")]
			string name,

			[Comments("Update modelType first. The property to bind to the name section.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
			string property,

			[Comments("Specify a format for the binding e.g. 'Value: {0:F2}'")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string modelType = null
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