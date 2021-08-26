using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMethodDictionaryDescriptor : Dictionary<string, string>
    {
        private List<ValidationMethodDescriptor> validationMethodDescriptors;

        public ValidationMethodDictionaryDescriptor(List<ValidationMethodDescriptor> validationMethodDescriptors) 
            => this.ValidationMethodDescriptors = validationMethodDescriptors;

        public ValidationMethodDictionaryDescriptor()
        {
        }

        public List<ValidationMethodDescriptor> ValidationMethodDescriptors
        {
            get => validationMethodDescriptors; 
            set
            {
                validationMethodDescriptors = value;
                this.Clear();
                validationMethodDescriptors.ForEach(vmd => this.Add(vmd.ClassName, vmd.Message));
            }
        }
    }
}
