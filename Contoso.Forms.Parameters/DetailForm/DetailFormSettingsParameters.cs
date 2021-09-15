using Contoso.Forms.Parameters.EditForm;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.DetailForm
{
    public class DetailFormSettingsParameters
    {
		public DetailFormSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Header field on the form")]
			string title,

			[Comments("List of fields and form groups for this form.")]
			List<DetailItemSettingsParameters> fieldSettings,

			[Comments("The model type for the object being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Includes the URL's for create, read, and update.")]
			EditFormRequestDetailsParameters requestDetails = null,

			[Comments("Multibindings list for the form header field.")]
			HeaderBindingsParameters headerBindings = null
		)
		{
			Title = title;
			RequestDetails = requestDetails;
			FieldSettings = fieldSettings;
			ModelType = modelType;
			HeaderBindings = headerBindings;
		}

		public string Title { get; set; }
		public EditFormRequestDetailsParameters RequestDetails { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
		public Type ModelType { get; set; }
		public HeaderBindingsParameters HeaderBindings { get; set; }
    }
}