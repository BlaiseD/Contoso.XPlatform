using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidatorArgumentDictionaryParameters : Dictionary<string, ValidatorArgumentParameters>
    {
		private List<ValidatorArgumentParameters> validatorArguments;

		public ValidatorArgumentDictionaryParameters
		(
			[Comments("")]
			List<ValidatorArgumentParameters> validatorArguments
		)
		{
			ValidatorArguments = validatorArguments;
		}

        public List<ValidatorArgumentParameters> ValidatorArguments
        {
            get => validatorArguments;
            set
            {
                validatorArguments = value;
                this.Clear();
                validatorArguments.ForEach(vad => this.Add(vad.Name, vad));
            }
        }
    }
}