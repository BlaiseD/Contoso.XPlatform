using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMessageDictionary : Dictionary<string, ValidationMethodDictionary>
    {
        private List<ValidationMessageDescriptor> validationMessageDescriptors;

        public ValidationMessageDictionary(List<ValidationMessageDescriptor> validationMessageDescriptors) 
            => this.ValidationMessageDescriptors = validationMessageDescriptors;

        public ValidationMessageDictionary()
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
                        new ValidationMethodDictionary(vmd.Methods)
                    )
                );
            }
        }
    }
}
