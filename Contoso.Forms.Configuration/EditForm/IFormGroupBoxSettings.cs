using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public interface IFormGroupBoxSettings
    {
        string GroupHeader { get; }
        List<FormItemSettingsDescriptor> FieldSettings { get; }
        MultiBindingDescriptor HeaderBindings { get; }
        bool IsHidden { get; }
    }
}
