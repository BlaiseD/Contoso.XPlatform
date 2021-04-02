using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class MultiSelectFormControlSettingsDescriptor : FormItemSettingsDescriptor
    {
        public override AbstractControlEnumDescriptor AbstractControlType => AbstractControlEnumDescriptor.MultiSelectFormControl;
        public List<string> KeyFields { get; set; }
        public MultiSelectTemplateDescriptor MultiSelectTemplate { get; set; }
    }
}
