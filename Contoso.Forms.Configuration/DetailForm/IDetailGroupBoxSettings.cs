using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DetailForm
{
    public interface IDetailGroupBoxSettings
    {
        string GroupHeader { get; }
        List<DetailItemSettingsDescriptor> FieldSettings { get; }
        MultiBindingDescriptor HeaderBindings { get; }
        bool IsHidden { get; }
    }
}
