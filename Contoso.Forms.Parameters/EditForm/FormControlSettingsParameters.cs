using Contoso.Forms.Parameters.Validation;
using System;

namespace Contoso.Forms.Parameters.EditForm
{
    public class FormControlSettingsParameters : FormItemSettingsParameters
    {
		public FormControlSettingsParameters(string domElementId, string title, string placeholder, string stringFormat, Type type, FieldValidationSettingsParameters validationSetting, TextFieldTemplateParameters textTemplate, DropDownTemplateParameters dropDownTemplate, string field) : base(field)
		{
			DomElementId = domElementId;
			Title = title;
			Placeholder = placeholder;
			StringFormat = stringFormat;
			Type = type;
			ValidationSetting = validationSetting;
			TextTemplate = textTemplate;
			DropDownTemplate = dropDownTemplate;
		}

		public string DomElementId { get; set; }
		public string Title { get; set; }
		public string Placeholder { get; set; }
		public string StringFormat { get; set; }
		public Type Type { get; set; }
		public FieldValidationSettingsParameters ValidationSetting { get; set; }
		public TextFieldTemplateParameters TextTemplate { get; set; }
		public DropDownTemplateParameters DropDownTemplate { get; set; }
    }
}