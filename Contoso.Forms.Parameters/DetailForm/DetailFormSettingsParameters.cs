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

			[Comments("Click the Variable button and select the configured DetailType enum field.")]
			DetailType detailType,

			[Comments("The model type for the object being edited. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("Includes the URL's for create, read, and update.")]
			FormRequestDetailsParameters requestDetails = null,

			[Comments("Multibindings list for the form header field.")]
			MultiBindingParameters headerBindings = null,

			[Comments("Multibindings list for the form header field.")]
			MultiBindingParameters subtitleBindings = null
		)
		{
			Title = title;
			RequestDetails = requestDetails;
			FieldSettings = fieldSettings;
			DetailType = detailType;
			ModelType = modelType;
			HeaderBindings = headerBindings;
			SubtitleBindings = subtitleBindings;
		}

		public string Title { get; set; }
		public FormRequestDetailsParameters RequestDetails { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
		public DetailType DetailType { get; set; }
		public Type ModelType { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
		public MultiBindingParameters SubtitleBindings { get; set; }
	}
}