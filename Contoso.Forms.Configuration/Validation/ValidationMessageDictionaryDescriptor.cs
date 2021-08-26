using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMessageDictionaryDescriptor : Dictionary<string, ValidationMethodDictionaryDescriptor>
    {
        private List<ValidationMessageDescriptor> validationMessageDescriptors;

        public ValidationMessageDictionaryDescriptor(List<ValidationMessageDescriptor> validationMessageDescriptors) 
            => this.ValidationMessageDescriptors = validationMessageDescriptors;

        public ValidationMessageDictionaryDescriptor()
        {
        }

        public List<ValidationMessageDescriptor> ValidationMessageDescriptors
        {
            get => validationMessageDescriptors; 
            set
            {
                validationMessageDescriptors = value;
                this.Clear();
                validationMessageDescriptors.ForEach
                (
                    vmd => this.Add
                    (
                        vmd.Field, 
                        new ValidationMethodDictionaryDescriptor(vmd.Methods)
                    )
                );
            }
        }
    }
}
