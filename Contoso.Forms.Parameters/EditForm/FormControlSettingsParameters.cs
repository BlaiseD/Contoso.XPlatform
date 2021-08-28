using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.EditForm
{
    public class FormControlSettingsParameters : FormItemSettingsParameters
    {
		public FormControlSettingsParameters
		(
			[Comments("")]
			string domElementId,

			[Comments("")]
			string title,

			[Comments("")]
			string placeholder,

			[Comments("")]
			string stringFormat,

			[Comments("")]
			Type type,

			[Comments("")]
			FieldValidationSettingsParameters validationSetting,

			[Comments("")]
			TextFieldTemplateParameters textTemplate,

			[Comments("")]
			DropDownTemplateParameters dropDownTemplate,

			[Comments("")]
			string field
		) : base(field)
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