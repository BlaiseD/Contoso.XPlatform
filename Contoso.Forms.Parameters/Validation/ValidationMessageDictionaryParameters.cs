﻿using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Validation
{
    public class ValidationMessageDictionaryParameters : Dictionary<string, ValidationMethodDictionaryParameters>
    {
		private List<ValidationMessageParameters> validationMessages;

		public ValidationMessageDictionaryParameters
		(
			[Comments("List of validation messages grouped by field. Each item includes a field and a corressponding list of validation methods.")]
			List<ValidationMessageParameters> validationMessages
		)
		{
			ValidationMessages = validationMessages;
		}

        public List<ValidationMessageParameters> ValidationMessages
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
                        new ValidationMethodDictionaryParameters(vmd.Methods)
                    )
                );
            }
        }
    }
}