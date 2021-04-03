using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Bsl.Configuration.ExpansionDescriptors
{
    public class SelectExpandDefinitionDescriptor
    {
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemDescriptor> ExpandedItems { get; set; } = new List<SelectExpandItemDescriptor>();
    }
}
