using Contoso.Forms.Parameters.Validation;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class MultiSelectFormControlSettingsParameters : FormControlSettingsParameters
    {
		public MultiSelectFormControlSettingsParameters
		(
			[Comments("")]
			List<string> keyFields,

			[Comments("")]
			MultiSelectTemplateParameters multiSelectTemplate,

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
		) : base(domElementId, title, placeholder, stringFormat, type, validationSetting, textTemplate, dropDownTemplate, field)
		{
			KeyFields = keyFields;
			MultiSelectTemplate = multiSelectTemplate;
		}

		public List<string> KeyFields { get; set; }
		public MultiSelectTemplateParameters MultiSelectTemplate { get; set; }
    }
}