using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public class EntityStateUpdater : IEntityStateUpdater
    {
        private readonly IMapper mapper;

        public EntityStateUpdater(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel GetUpdatedModel<TModel>(TModel existingEntity, ObservableCollection<IValidatable> modifiedProperties, List<FormItemSettingsDescriptor> fieldSettings)
        {
            Dictionary<string, object> existing = existingEntity.EntityToObjectDictionary
            (
               mapper,
               fieldSettings
            );

            Dictionary<string, object> current = modifiedProperties.ValidatableListToObjectDictionary
            (
                mapper,
                fieldSettings
            );

            EntityMapper.UpdateEntityStates
            (
                existing,
                current,
                fieldSettings
            );

            return mapper.Map<TModel>(current);
        }
    }
}
