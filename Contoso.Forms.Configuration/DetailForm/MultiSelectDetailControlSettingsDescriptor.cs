using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public class MultiSelectDetailControlSettingsDescriptor : DetailControlSettingsDescriptor
    {
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateDescriptor MultiSelectTemplate { get; set; }
    }
}
