using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Directives
{
    public class VariableDirectivesDictionaryDescriptor : Dictionary<string, List<DirectiveDescriptor>>
    {
        private List<VariableDirectivesDescriptor> variableDirectives;

        public VariableDirectivesDictionaryDescriptor(List<VariableDirectivesDescriptor> variableDirectivesDescriptors) 
            => this.VariableDirectives = variableDirectivesDescriptors;

        public VariableDirectivesDictionaryDescriptor()
        {
        }

        public List<VariableDirectivesDescriptor> VariableDirectives
        {
            get => variableDirectives; 
            set
            {
                variableDirectives = value;
                this.Clear();
                variableDirectives.ForEach(vdd => this.Add(vdd.Field, vdd.ConditionalDirectives));
            }
        }
    }
}
