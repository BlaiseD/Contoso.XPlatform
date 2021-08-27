using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class TextGroupParameters
    {
		public TextGroupParameters(string title, List<LabelItemParametersBase> labels)
		{
			Title = title;
			Labels = labels;
		}

		public string Title { get; set; }
		public List<LabelItemParametersBase> Labels { get; set; }
    }
}