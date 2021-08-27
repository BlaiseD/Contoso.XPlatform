using Contoso.Forms.Parameters.Validation;
using System;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.EditForm
{
    public class MultiSelectFormControlSettingsParameters : FormControlSettingsParameters
    {
		public MultiSelectFormControlSettingsParameters(List<string> keyFields, MultiSelectTemplateParameters multiSelectTemplate, string domElementId, string title, string placeholder, string stringFormat, Type type, FieldValidationSettingsParameters validationSetting, TextFieldTemplateParameters textTemplate, DropDownTemplateParameters dropDownTemplate, string field) : base(domElementId, title, placeholder, stringFormat, type, validationSetting, textTemplate, dropDownTemplate, field)
		{
			KeyFields = keyFields;
			MultiSelectTemplate = multiSelectTemplate;
		}

		public List<string> KeyFields { get; set; }
		public MultiSelectTemplateParameters MultiSelectTemplate { get; set; }
    }
}