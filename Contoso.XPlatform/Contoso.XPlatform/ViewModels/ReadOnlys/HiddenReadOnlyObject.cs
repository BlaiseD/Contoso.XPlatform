using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(string name, FormControlSettingsDescriptor setting, IContextProvider contextProvider) : base(name, setting.TextTemplate.TemplateName, contextProvider.UiNotificationService)
        {
        }
    }
}
