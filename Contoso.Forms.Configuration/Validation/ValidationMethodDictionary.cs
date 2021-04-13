using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationMethodDictionary : Dictionary<string, string>
    {
        private List<ValidationMethodDescriptor> validationMethodDescriptors;

        public ValidationMethodDictionary(List<ValidationMethodDescriptor> validationMethodDescriptors) 
            => this.ValidationMethodDescriptors = validationMethodDescriptors;

        public ValidationMethodDictionary()
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
