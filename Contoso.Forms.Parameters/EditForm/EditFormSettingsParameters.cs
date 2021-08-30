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
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "modelType")]
			[Comments("Update displayFieldTypeSource first. This field may be displayed next to the title - empty on Add.")]
			string displayField,

			[Comments("")]
			EditFormRequestDetailsParameters requestDetails,

			[Comments("")]
			ValidationMessageDictionaryParameters validationMessages,

			[Comments("")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("")]
			Type modelType,

			[Comments("")]
			string validFormControlText,

			[Comments("")]
			string invalidFormControlText,

			[Comments("")]
			VariableDirectivesDictionaryParameters conditionalDirectives = null,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string displayFieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Title = title;
			DisplayField = displayField;
			RequestDetails = requestDetails;
			ValidationMessages = validationMessages;
			FieldSettings = fieldSettings;
			ModelType = modelType;
			ValidFormControlText = validFormControlText;
			InvalidFormControlText = invalidFormControlText;
			ConditionalDirectives = conditionalDirectives;
		}

		public string Title { get; set; }
		public string DisplayField { get; set; }
		public EditFormRequestDetailsParameters RequestDetails { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public Type ModelType { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
    }
}