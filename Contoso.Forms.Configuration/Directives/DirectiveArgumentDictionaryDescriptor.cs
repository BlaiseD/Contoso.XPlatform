using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Directives
{
    public class DirectiveArgumentDictionaryDescriptor : Dictionary<string, DirectiveArgumentDescriptor>
    {
        private List<DirectiveArgumentDescriptor> directiveArgumentDescriptors;

        public DirectiveArgumentDictionaryDescriptor(List<DirectiveArgumentDescriptor> directiveArgumentDescriptors) 
            => this.DirectiveArgumentDescriptors = directiveArgumentDescriptors;

        public DirectiveArgumentDictionaryDescriptor()
        {
        }

        public List<DirectiveArgumentDescriptor> DirectiveArgumentDescriptors
        {
            get => directiveArgumentDescriptors; 
            set
            {
                directiveArgumentDescriptors = value;
                this.Clear();
                directiveArgumentDescriptors.ForEach(dad => this.Add(dad.Name, dad));
            }
        }
    }
}
