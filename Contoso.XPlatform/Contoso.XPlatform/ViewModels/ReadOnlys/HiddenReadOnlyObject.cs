using Contoso.Forms.Configuration.DataForm;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(string name, FormControlSettingsDescriptor setting) : base(name, setting.TextTemplate.TemplateName)
        {
        }
    }
}
