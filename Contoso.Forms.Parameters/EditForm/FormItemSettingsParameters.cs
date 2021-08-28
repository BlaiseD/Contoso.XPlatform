using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.EditForm
{
    abstract public class FormItemSettingsParameters
    {
		public FormItemSettingsParameters
		(
			[Comments("")]
			string field
		)
		{
			Field = field;
		}

		public string Field { get; set; }
    }
}