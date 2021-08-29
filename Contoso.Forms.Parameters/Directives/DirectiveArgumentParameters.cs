using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveArgumentParameters
    {
		public DirectiveArgumentParameters
		(
			[Comments("Name of the argument for the directive method.")]
			string name,

			[Comments("Value of the argument for the directive method.")]
			object value,

			[Comments("Assembly qualified type name for the argument.  The full name (e.g. System.Int32) is sufficient for literals.")]
			[ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
			Type type
		)
		{
			Name = name;
			Value = value;
			Type = type;
		}

		public string Name { get; set; }
		public object Value { get; set; }
		public Type Type { get; set; }
    }
}