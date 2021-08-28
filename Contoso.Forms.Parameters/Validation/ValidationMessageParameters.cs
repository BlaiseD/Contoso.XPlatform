﻿using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationMessageParameters
    {
		public ValidationMessageParameters
		(
			[Comments("")]
			string field,

			[Comments("")]
			List<ValidationMethodParameters> methods
		)
		{
			Field = field;
			Methods = methods;
		}

		public string Field { get; set; }
		public List<ValidationMethodParameters> Methods { get; set; }
    }
}