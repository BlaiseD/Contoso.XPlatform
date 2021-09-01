using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationRuleParameters
    {
		public ValidationRuleParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "RequiredRule")]
			[Comments("The validation class")]
			string className,

			[Comments("The validtion message")]
			[NameValue(AttributeNames.DEFAULTVALUE, "(Property) is required.")]
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