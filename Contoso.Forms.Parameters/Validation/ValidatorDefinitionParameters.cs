using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidatorDefinitionParameters
    {
		public ValidatorDefinitionParameters
		(
			[Comments("Validation class name.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "RequiredRule")]
			string className,

			[Comments("Function to call on the validation class.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Check")]
			string functionName,

			[Comments("Where applicable, add arguments for the validator function e.g. min, max vallues.")]
			ValidatorArgumentDictionaryParameters arguments = null
		)
		{
			ClassName = className;
			FunctionName = functionName;
			Arguments = arguments;
		}

		public string ClassName { get; set; }
		public string FunctionName { get; set; }
		public ValidatorArgumentDictionaryParameters Arguments { get; set; }
    }
}