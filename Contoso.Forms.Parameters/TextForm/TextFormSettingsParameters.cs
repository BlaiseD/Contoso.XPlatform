using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class TextFormSettingsParameters
    {
		public TextFormSettingsParameters
		(
			[Comments("")]
			string title,

			[Comments("")]
			List<TextGroupParameters> textGroups
		)
		{
			Title = title;
			TextGroups = textGroups;
		}

		public string Title { get; set; }
		public List<TextGroupParameters> TextGroups { get; set; }
    }
}