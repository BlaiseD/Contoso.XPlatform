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
			[Comments("")]
			string title,

			[Comments("")]
			string displayField,

			[Comments("")]
			EditFormRequestDetailsParameters requestDetails,

			[Comments("")]
			ValidationMessageDictionaryParameters validationMessages,

			[Comments("")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("")]
			VariableDirectivesDictionaryParameters conditionalDirectives,

			[Comments("")]
			Type modelType,

			[Comments("")]
			string validFormControlText,

			[Comments("")]
			string invalidFormControlText
		)
		{
			Title = title;
			DisplayField = displayField;
			RequestDetails = requestDetails;
			ValidationMessages = validationMessages;
			FieldSettings = fieldSettings;
			ConditionalDirectives = conditionalDirectives;
			ModelType = modelType;
			ValidFormControlText = validFormControlText;
			InvalidFormControlText = invalidFormControlText;
		}

		public string Title { get; set; }
		public string DisplayField { get; set; }
		public EditFormRequestDetailsParameters RequestDetails { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
		public Type ModelType { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
    }
}