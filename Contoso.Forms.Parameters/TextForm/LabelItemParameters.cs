using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.TextForm
{
    public class LabelItemParameters : LabelItemParametersBase
    {
		public LabelItemParameters
		(
			[Comments("")]
			string text
		)
		{
			Text = text;
		}

		public string Text { get; set; }
    }
}