using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveDefinitionParameters
    {
		public DirectiveDefinitionParameters
		(
			[Comments("Class name for the directive.")]
			[Domain("DisableIf,HideIf,ValidateIf")]
			[NameValue(AttributeNames.DEFAULTVALUE, "HideIf")]
			string className,

			[Comments("Function name.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Check")]
			[Domain("Check")]
			string functionName,

			[Comments("Where applicable, add arguments for the directive evaluation function.")]
			DirectiveArgumentDictionaryParameters arguments = null
		)
		{
			ClassName = className;
			FunctionName = functionName;
			Arguments = arguments;
		}

		public string ClassName { get; set; }
		public string FunctionName { get; set; }
		public DirectiveArgumentDictionaryParameters Arguments { get; set; }
    }
}