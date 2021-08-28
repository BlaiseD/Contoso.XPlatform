using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class CommandButtonParameters
    {
		public CommandButtonParameters
		(
			[Comments("")]
			int id,

			[Comments("")]
			string shortString,

			[Comments("")]
			string longString,

			[Comments("")]
			string command,

			[Comments("")]
			string buttonIcon
		)
		{
			Id = id;
			ShortString = shortString;
			LongString = longString;
			Command = command;
			ButtonIcon = buttonIcon;
		}

		public int Id { get; set; }
		public string ShortString { get; set; }
		public string LongString { get; set; }
		public string Command { get; set; }
		public string ButtonIcon { get; set; }
    }
}