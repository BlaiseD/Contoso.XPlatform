using Contoso.Forms.Configuration.DetailForm;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, object entity, List<DetailItemSettingsDescriptor> fieldSettings, string parentField = null);
    }
}
