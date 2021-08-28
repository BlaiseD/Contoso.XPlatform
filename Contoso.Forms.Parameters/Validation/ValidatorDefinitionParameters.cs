using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidatorDefinitionParameters
    {
		public ValidatorDefinitionParameters
		(
			[Comments("")]
			string className,

			[Comments("")]
			string functionName,

			[Comments("")]
			ValidatorArgumentDictionaryParameters arguments
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