namespace Contoso.Forms.Parameters.TextForm
{
    public class HyperLinkSpanItemParameters : SpanItemParametersBase
    {
		public HyperLinkSpanItemParameters(string text, string url)
		{
			Text = text;
			Url = url;
		}

		public string Text { get; set; }
		public string Url { get; set; }
    }
}