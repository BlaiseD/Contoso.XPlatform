namespace Contoso.Forms.Parameters.TextForm
{
    public class SpanItemParameters : SpanItemParametersBase
    {
		public SpanItemParameters(string text)
		{
			Text = text;
		}

		public string Text { get; set; }
    }
}