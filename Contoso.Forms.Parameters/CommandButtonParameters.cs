using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class CommandButtonParameters
    {
		public CommandButtonParameters
		(
			[Comments("")]
			string command,

			[Comments("")]
			string buttonIcon
		)
		{
			Command = command;
			ButtonIcon = buttonIcon;
		}

		public string Command { get; set; }
		public string ButtonIcon { get; set; }
    }
}