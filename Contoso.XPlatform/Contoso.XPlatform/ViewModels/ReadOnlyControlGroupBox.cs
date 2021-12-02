﻿using Contoso.Forms.Configuration;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.ViewModels
{
    public class ReadOnlyControlGroupBox : ObservableCollection<IReadOnly>
    {
        public ReadOnlyControlGroupBox(string groupHeader, MultiBindingDescriptor headerBindings, IEnumerable<IReadOnly> collection) : base(collection)
        {
            GroupHeader = groupHeader;
            HeaderBindings = headerBindings;
        }

        public string GroupHeader { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public Dictionary<string, IReadOnly> BindingPropertiesDictionary
            => this.ToDictionary(p => p.Name.ToBindingDictionaryKey());
    }
}
