using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public class DetailGroupBoxSettingsDescriptor : DetailItemSettingsDescriptor
    {
        public string GroupHeader { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
    }
}
