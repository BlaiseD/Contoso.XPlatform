using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.DetailForm
{
    public class DetailGroupBoxSettingsParameters : DetailItemSettingsParameters
	{
		public DetailGroupBoxSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Header")]
			[Comments("Title for the group box.")]
			string groupHeader,

			[Comments("Configuration for each field in the group box.")]
			List<DetailItemSettingsParameters> fieldSettings,

			[Comments("Multibindings list for the group header field.")]
			MultiBindingParameters headerBindings = null
		)
		{
			if (fieldSettings.Any(s => s is DetailGroupBoxSettingsParameters))
				throw new ArgumentException($"{nameof(fieldSettings)}: 83D45CDE-DF1E-4CA4-9F97-8D6F802762A4");

			GroupHeader = groupHeader;
			FieldSettings = fieldSettings;
			HeaderBindings = headerBindings;
		}

		public string GroupHeader { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
	}
}
