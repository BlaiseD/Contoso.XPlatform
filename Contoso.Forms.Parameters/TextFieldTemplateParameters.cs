namespace Contoso.Forms.Parameters
{
    public class TextFieldTemplateParameters
    {
		public TextFieldTemplateParameters(string templateName)
		{
			TemplateName = templateName;
		}

		public string TemplateName { get; set; }
    }
}