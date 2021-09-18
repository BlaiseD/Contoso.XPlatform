using Contoso.Forms.Configuration.EditForm;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public class DetailFormSettingsDescriptor : IDetailGroupSettings
    {
        public string Title { get; set; }
        public EditFormRequestDetailsDescriptor RequestDetails { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
        public DetailType DetailType { get; set; }
        public string ModelType { get; set; }
        public MultiBindingDescriptor HeaderBindings { get; set; }
        public MultiBindingDescriptor SubtitleBindings { get; set; }
    }
}
