using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class FormGroupSettingsParameters : FormItemSettingsParameters
    {
		public FormGroupSettingsParameters(string title, string validFormControlText, string invalidFormControlText, bool showTitle, string modelType, FormGroupTemplateParameters formGroupTemplate, List<FormItemSettingsParameters> fieldSettings, ValidationMessageDictionaryParameters validationMessages, VariableDirectivesDictionaryParameters conditionalDirectives, string field) : base(field)
		{
			Title = title;
			ValidFormControlText = validFormControlText;
			InvalidFormControlText = invalidFormControlText;
			ShowTitle = showTitle;
			ModelType = modelType;
			FormGroupTemplate = formGroupTemplate;
			FieldSettings = fieldSettings;
			ValidationMessages = validationMessages;
			ConditionalDirectives = conditionalDirectives;
		}

		public string Title { get; set; }
		public string ValidFormControlText { get; set; }
		public string InvalidFormControlText { get; set; }
		public bool ShowTitle { get; set; }
		public string ModelType { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
    }
}