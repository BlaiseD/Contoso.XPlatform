using Contoso.Forms.Parameters.Directives;
using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class FormGroupSettingsParameters : FormItemSettingsParameters
    {
		public FormGroupSettingsParameters
		(
			[Comments("")]
			string title,

			[Comments("")]
			string validFormControlText,

			[Comments("")]
			string invalidFormControlText,

			[Comments("")]
			bool showTitle,

			[Comments("")]
			Type modelType,

			[Comments("")]
			FormGroupTemplateParameters formGroupTemplate,

			[Comments("")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("")]
			ValidationMessageDictionaryParameters validationMessages,

			[Comments("")]
			VariableDirectivesDictionaryParameters conditionalDirectives,

			[Comments("")]
			string field
		) : base(field)
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
		public Type ModelType { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public ValidationMessageDictionaryParameters ValidationMessages { get; set; }
		public VariableDirectivesDictionaryParameters ConditionalDirectives { get; set; }
    }
}