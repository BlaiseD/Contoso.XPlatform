using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.DetailForm
{
    public class DetailMultiSelectFormControlSettingsParameters : DetailControlSettingsParameters
    {
		public DetailMultiSelectFormControlSettingsParameters
		(

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Source property name from the target object.")]
			string field,

			[Comments("Label for the field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			string title,

			[Comments("May need to remove (doubtful it is useful for a multiselect which has text display fields). String format - useful for binding decimals.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[Comments("The type for the field being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.  Use the full name (e.g. System.Int32) for literals or core platform types.")]
			Type type,

			[Comments("Holds the XAML template name for the field plus additional multi-select related properties (textField, valueField, request details etc.).  Use for getting the text properties given the value proerties.")]
			MultiSelectTemplateParameters multiSelectTemplate,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		) : base(field, title, stringFormat, type, null, null)
		{
			MultiSelectTemplate = multiSelectTemplate;
		}

		public MultiSelectTemplateParameters MultiSelectTemplate { get; set; }
    }
}