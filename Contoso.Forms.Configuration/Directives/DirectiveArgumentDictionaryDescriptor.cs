using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Directives
{
    public class DirectiveArgumentDictionaryDescriptor : Dictionary<string, DirectiveArgumentDescriptor>
    {
        private List<DirectiveArgumentDescriptor> directiveArguments;

        public DirectiveArgumentDictionaryDescriptor(List<DirectiveArgumentDescriptor> directiveArguments) 
            => this.DirectiveArguments = directiveArguments;

        public DirectiveArgumentDictionaryDescriptor()
        {
        }

        public List<DirectiveArgumentDescriptor> DirectiveArguments
        {
            get => directiveArguments; 
            set
            {
                directiveArguments = value;
                this.Clear();
                directiveArguments.ForEach(dad => this.Add(dad.Name, dad));
            }
        }
    }
}
