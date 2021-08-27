namespace Contoso.Forms.Parameters.Validation
{
    public class ValidatorDefinitionParameters
    {
		public ValidatorDefinitionParameters(string className, string functionName, ValidatorArgumentDictionaryParameters arguments)
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