using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class TextGroupParameters
    {
		public TextGroupParameters
		(
			[Comments("")]
			string title,

			[Comments("")]
			List<LabelItemParametersBase> labels
		)
		{
			Title = title;
			Labels = labels;
		}

		public string Title { get; set; }
		public List<LabelItemParametersBase> Labels { get; set; }
    }
}