using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class FieldValidationSettingsParameters
    {
		public FieldValidationSettingsParameters(object defaultValue, List<ValidatorDefinitionParameters> validators)
		{
			DefaultValue = defaultValue;
			Validators = validators;
		}

		public object DefaultValue { get; set; }
		public List<ValidatorDefinitionParameters> Validators { get; set; }
    }
}