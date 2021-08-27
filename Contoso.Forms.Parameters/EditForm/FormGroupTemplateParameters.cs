namespace Contoso.Forms.Parameters.EditForm
{
    public class FormGroupTemplateParameters
    {
		public FormGroupTemplateParameters(string templateName)
		{
			TemplateName = templateName;
		}

		public string TemplateName { get; set; }
    }
}