using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidatorArgumentDictionary : Dictionary<string, ValidatorArgumentDescriptor>
    {
        private List<ValidatorArgumentDescriptor> validatorArgumentDescriptors;

        public ValidatorArgumentDictionary(List<ValidatorArgumentDescriptor> validatorArgumentDescriptors)
            => this.ValidatorArgumentDescriptors = validatorArgumentDescriptors;

        public ValidatorArgumentDictionary()
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
