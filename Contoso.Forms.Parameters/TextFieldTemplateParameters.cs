using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class TextFieldTemplateParameters
    {
		public TextFieldTemplateParameters
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