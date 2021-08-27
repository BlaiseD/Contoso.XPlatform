using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class TextFormSettingsParameters
    {
		public TextFormSettingsParameters(string title, List<TextGroupParameters> textGroups)
		{
			Title = title;
			TextGroups = textGroups;
		}

		public string Title { get; set; }
		public List<TextGroupParameters> TextGroups { get; set; }
    }
}