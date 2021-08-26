using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Directives
{
    public class VariableDirectivesDictionaryDescriptor : Dictionary<string, List<DirectiveDescriptor>>
    {
        private List<VariableDirectivesDescriptor> variableDirectivesDescriptors;

        public VariableDirectivesDictionaryDescriptor(List<VariableDirectivesDescriptor> variableDirectivesDescriptors) 
            => this.VariableDirectivesDescriptors = variableDirectivesDescriptors;

        public VariableDirectivesDictionaryDescriptor()
        {
        }

        public List<VariableDirectivesDescriptor> VariableDirectivesDescriptors
        {
            get => variableDirectivesDescriptors; 
            set
            {
                variableDirectivesDescriptors = value;
                this.Clear();
                variableDirectivesDescriptors.ForEach(vdd => this.Add(vdd.Field, vdd.ConditionalDirectives));
            }
        }
    }
}
