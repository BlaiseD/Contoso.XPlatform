using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationRuleDictionaryParameters : Dictionary<string, string>
    {
		private List<ValidationRuleParameters> validationRules;

		public ValidationRuleDictionaryParameters
		(
			[Comments("List of validation rules for a given field.")]
			List<ValidationRuleParameters> validationRules
        )
		{
			ValidationRules = validationRules;
		}

        public List<ValidationRuleParameters> ValidationRules
        {
            get => validationRules;
            set
            {
                validationRules = value;
                this.Clear();
                validationRules.ForEach(vrp => this.Add(vrp.ClassName, vrp.Message));
            }
        }
    }
}