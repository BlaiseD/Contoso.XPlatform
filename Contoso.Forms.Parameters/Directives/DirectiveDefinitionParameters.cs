using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveDefinitionParameters
    {
		public DirectiveDefinitionParameters
		(
			[Comments("")]
			string className,

			[Comments("")]
			string functionName,

			[Comments("")]
			DirectiveArgumentDictionaryParameters arguments
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