using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Validation
{
    public class ValidationRuleDictionaryDescriptor : Dictionary<string, string>
    {
        private List<ValidationRuleDescriptor> validationRules;

        public ValidationRuleDictionaryDescriptor(List<ValidationRuleDescriptor> validationRuleDescriptors) 
            => this.ValidationRules = validationRuleDescriptors;

        public ValidationRuleDictionaryDescriptor()
        {
        }

        public List<ValidationRuleDescriptor> ValidationRules
        {
            get => validationRules; 
            set
            {
                validationRules = value;
                this.Clear();
                validationRules.ForEach(vrd => this.Add(vrd.ClassName, vrd.Message));
            }
        }
    }
}
