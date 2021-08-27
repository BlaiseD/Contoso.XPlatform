using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Directives
{
    public class VariableDirectivesDictionaryParameters : Dictionary<string, List<DirectiveParameters>>
    {
        private List<VariableDirectivesParameters> variableDirectives;

        public VariableDirectivesDictionaryParameters(List<VariableDirectivesParameters> variableDirectives)
        {
            VariableDirectives = variableDirectives;
        }

        public List<VariableDirectivesParameters> VariableDirectives
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