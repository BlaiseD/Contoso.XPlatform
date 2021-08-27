using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMessageDictionaryDescriptor : Dictionary<string, ValidationMethodDictionaryDescriptor>
    {
        private List<ValidationMessageDescriptor> validationMessages;

        public ValidationMessageDictionaryDescriptor(List<ValidationMessageDescriptor> validationMessageDescriptors) 
            => this.ValidationMessages = validationMessageDescriptors;

        public ValidationMessageDictionaryDescriptor()
        {
        }

        public List<ValidationMessageDescriptor> ValidationMessages
        {
            get => validationMessages; 
            set
            {
                validationMessages = value;
                this.Clear();
                validationMessages.ForEach
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
