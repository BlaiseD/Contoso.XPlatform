using System.Collections.Generic;

namespace Contoso.Forms.Parameters.TextForm
{
    public class FormattedLabelItemParameters : LabelItemParametersBase
    {
		public FormattedLabelItemParameters(List<SpanItemParametersBase> items)
		{
			Items = items;
		}

		public List<SpanItemParametersBase> Items { get; set; }
    }
}