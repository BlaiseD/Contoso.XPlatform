using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMethodDictionaryDescriptor : Dictionary<string, string>
    {
        private List<ValidationMethodDescriptor> validationMethods;

        public ValidationMethodDictionaryDescriptor(List<ValidationMethodDescriptor> validationMethodDescriptors) 
            => this.ValidationMethods = validationMethodDescriptors;

        public ValidationMethodDictionaryDescriptor()
        {
        }

        public List<ValidationMethodDescriptor> ValidationMethods
        {
            get => validationMethods; 
            set
            {
                validationMethods = value;
                this.Clear();
                validationMethods.ForEach(vmd => this.Add(vmd.ClassName, vmd.Message));
            }
        }
    }
}
