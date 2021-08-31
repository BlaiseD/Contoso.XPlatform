using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class EditFormSettingsParameters
    {
		public EditFormSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Header field on the form")]
			string title,

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. This field may be displayed next to the title - empty on Add.")]
			string displayField,

			[Comments("Includes the URL's for create, read, and update.")]
			EditFormRequestDetailsParameters requestDetails,

			[Comments("Input validation messages for each field.")]
			ValidationMessageDictionaryParameters validationMessages,

			[Comments("List of fields and form groups for this form.")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("Click the Variable button and select the configured EditType enum field.")]
			EditType editType,

			[Comments("The model type for the object being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Conditional directtives for each field.")]
			VariableDirectivesDictionaryParameters conditionalDirectives = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Title = title;
			DisplayField = displayField;
			RequestDetails = requestDetails;
			ValidationMessages = validationMessages;
			FieldSettings = fieldSettings;
			EditType = editType;
			ModelType = modelType;
			ConditionalDirectives = conditionalDirectives;
		}

		public string Title { get; set; }
		public string DisplayField { get; set; }
		public EditFormRequestDetailsParameters RequestDetails { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public EditType EditType { get; set; }
		public Type ModelType { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
    }
}