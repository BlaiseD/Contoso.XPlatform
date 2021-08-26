using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidatorArgumentDictionaryDescriptor : Dictionary<string, ValidatorArgumentDescriptor>
    {
        private List<ValidatorArgumentDescriptor> validatorArgumentDescriptors;

        public ValidatorArgumentDictionaryDescriptor(List<ValidatorArgumentDescriptor> validatorArgumentDescriptors)
            => this.ValidatorArgumentDescriptors = validatorArgumentDescriptors;

        public ValidatorArgumentDictionaryDescriptor()
        {
        }

        public List<ValidatorArgumentDescriptor> ValidatorArgumentDescriptors
        {
            get => validatorArgumentDescriptors;
            set
            {
                validatorArgumentDescriptors = value;
                this.Clear();
                validatorArgumentDescriptors.ForEach(vad => this.Add(vad.Name, vad));
            }
        }
    }
}
