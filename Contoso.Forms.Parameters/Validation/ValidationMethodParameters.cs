using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationMethodParameters
    {
		public ValidationMethodParameters
		(
			[Comments("")]
			string className,

			[Comments("")]
			string message
		)
		{
			ClassName = className;
			Message = message;
		}

		public string ClassName { get; set; }
		public string Message { get; set; }
    }
}