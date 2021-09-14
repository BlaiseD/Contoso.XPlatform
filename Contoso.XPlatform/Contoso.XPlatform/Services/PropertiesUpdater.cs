using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public class PropertiesUpdater : IPropertiesUpdater
    {
        private readonly IMapper mapper;

        public PropertiesUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void UpdateProperties(IEnumerable<IValidatable> properties, object entity, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null)
        {
            properties.UpdateValidatables(entity, fieldSettings, mapper, parentField);
        }
    }
}
