using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveArgumentParameters
    {
		public DirectiveArgumentParameters
		(
			[Comments("")]
			string name,

			[Comments("")]
			object value,

			[Comments("")]
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