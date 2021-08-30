using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.ItemFilter
{
    public class MemberSourceFilterParameters : ItemFilterParametersBase
    {
		public MemberSourceFilterParameters
		(
			[Comments("")]
			string field,

			[Comments("The filter operator (comparison).")]
			[Domain("eq, neq")]
			[ParameterEditorControl(ParameterControlType.DropDown)]
			string oper,

			[Comments("")]
			string memberSource,

			[Comments("")]
			Type type
		)
		{
			Field = field;
			Operator = oper;
			MemberSource = memberSource;
			Type = type;
		}

		public string Field { get; set; }
		public string Operator { get; set; }
		public string MemberSource { get; set; }
		public Type Type { get; set; }
    }
}