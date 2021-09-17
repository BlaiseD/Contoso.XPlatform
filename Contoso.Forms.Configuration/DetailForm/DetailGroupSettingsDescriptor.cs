using Contoso.Forms.Configuration.EditForm;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public class DetailGroupSettingsDescriptor : DetailItemSettingsDescriptor, IChildDetailGroupSettings
    {
        public string Title { get; set; }
        public string ModelType { get; set; }
        public FormGroupTemplateDescriptor FormGroupTemplate { get; set; }
        public List<DetailItemSettingsDescriptor> FieldSettings { get; set; }
    }
}
