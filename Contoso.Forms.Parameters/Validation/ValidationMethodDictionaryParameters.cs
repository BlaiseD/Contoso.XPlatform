using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationMethodDictionaryParameters : Dictionary<string, string>
    {
		private List<ValidationMethodParameters> validationMethods;

		public ValidationMethodDictionaryParameters
		(
			[Comments("")]
			List<ValidationMethodParameters> validationMethods
		)
		{
			ValidationMethods = validationMethods;
		}

        public List<ValidationMethodParameters> ValidationMethods
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