using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class EditFormSettingsParameters
    {
		public EditFormSettingsParameters(string title, string displayField, FormGroupTemplateParameters formGroupTemplate, EditFormRequestDetailsParameters requestDetails, ValidationMessageDictionaryParameters validationMessages, List<FormItemSettingsParameters> fieldSettings, VariableDirectivesDictionaryParameters conditionalDirectives, string modelType, string validFormControlText, string invalidFormControlText)
		{
			Title = title;
			DisplayField = displayField;
			FormGroupTemplate = formGroupTemplate;
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
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public EditFormRequestDetailsParameters RequestDetails { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
		public string ModelType { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
    }
}