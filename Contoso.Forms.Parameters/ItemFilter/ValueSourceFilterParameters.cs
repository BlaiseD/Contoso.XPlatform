using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.ItemFilter
{
    public class ValueSourceFilterParameters : ItemFilterParametersBase
    {
		public ValueSourceFilterParameters
		(
			[Comments("")]
			string field,

			[Comments("The filter operator (comparison).")]
			[Domain("eq, neq")]
			[ParameterEditorControl(ParameterControlType.DropDown)]
			string oper,

			[Comments("")]
			object value,

			[Comments("")]
			Type type
		)
		{
			Field = field;
			Operator = oper;
			Value = value;
			Type = type;
		}

		public string Field { get; set; }
		public string Operator { get; set; }
		public object Value { get; set; }
		public Type Type { get; set; }
    }
}