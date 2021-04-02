using System.Collections.Generic;

namespace Contoso.Forms.Configuration.Directives
{
    public class DirectiveArgumentDictionary : Dictionary<string, DirectiveArgumentDescriptor>
    {
        private List<DirectiveArgumentDescriptor> directiveArgumentDescriptors;

        public DirectiveArgumentDictionary(List<DirectiveArgumentDescriptor> directiveArgumentDescriptors) 
            => this.DirectiveArgumentDescriptors = directiveArgumentDescriptors;

        public DirectiveArgumentDictionary()
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
