namespace Contoso.Forms.Parameters.TextForm
{
    public class LabelItemParameters : LabelItemParametersBase
    {
		public LabelItemParameters(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
    }
}