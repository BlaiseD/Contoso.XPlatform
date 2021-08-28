using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.EditForm
{
    public class FormGroupTemplateParameters
    {
		public FormGroupTemplateParameters
		(
			[Comments("")]
			string templateName
		)
		{
			TemplateName = templateName;
		}

		public string TemplateName { get; set; }
    }
}