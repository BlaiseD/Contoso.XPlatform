using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidatorArgumentDictionaryDescriptor : Dictionary<string, ValidatorArgumentDescriptor>
    {
        private List<ValidatorArgumentDescriptor> validatorArguments;

        public ValidatorArgumentDictionaryDescriptor(List<ValidatorArgumentDescriptor> validatorArgumentDescriptors)
            => this.ValidatorArguments = validatorArgumentDescriptors;

        public ValidatorArgumentDictionaryDescriptor()
        {
        }

        public List<ValidatorArgumentDescriptor> ValidatorArguments
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
