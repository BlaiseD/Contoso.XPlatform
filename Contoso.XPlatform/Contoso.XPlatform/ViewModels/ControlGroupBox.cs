using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.ViewModels
{
    public class ControlGroupBox : ObservableCollection<IValidatable>
    {
        public ControlGroupBox(string groupHeader, MultiBindingDescriptor headerBindings, IEnumerable<IValidatable> collection) : base(collection)
        {
            GroupHeader = groupHeader;
            HeaderBindings = headerBindings;
        }

        public string GroupHeader { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public Dictionary<string, IValidatable> BindingPropertiesDictionary
            => this.ToDictionary(p => p.Name.ToBindingDictionaryKey());
    }
}
