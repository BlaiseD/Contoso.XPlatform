using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class FormGroupArraySettingsParameters : FormItemSettingsParameters
    {
		public FormGroupArraySettingsParameters(string title, string placeholder, bool showTitle, string modelType, string type, string validFormControlText, string invalidFormControlText, List<string> keyFields, FormsCollectionDisplayTemplateParameters formsCollectionDisplayTemplate, FormGroupTemplateParameters formGroupTemplate, List<FormItemSettingsParameters> fieldSettings, ValidationMessageDictionaryParameters validationMessages, VariableDirectivesDictionaryParameters conditionalDirectives, string field) : base(field)
		{
			Title = title;
			Placeholder = placeholder;
			ShowTitle = showTitle;
			ModelType = modelType;
			Type = type;
			ValidFormControlText = validFormControlText;
			InvalidFormControlText = invalidFormControlText;
			KeyFields = keyFields;
			FormsCollectionDisplayTemplate = formsCollectionDisplayTemplate;
			FormGroupTemplate = formGroupTemplate;
			FieldSettings = fieldSettings;
			ValidationMessages = validationMessages;
			ConditionalDirectives = conditionalDirectives;
		}

		public string Title { get; set; }
		public string Placeholder { get; set; }
		public bool ShowTitle { get; set; }
		public string ModelType { get; set; }
		public string Type { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
		public List<string> KeyFields { get; set; }
		public FormsCollectionDisplayTemplateParameters FormsCollectionDisplayTemplate { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
    }
}