using AutoMapper;

namespace Contoso.XPlatform.Services
{
    public class Utilities : IUtilities
    {
        public Utilities(IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder, IEntityStateUpdater entityStateUpdater, IEntityUpdater entityUpdater, IFieldsCollectionBuilder fieldsCollectionBuilder, IGetItemFilterBuilder getItemFilterBuilder, IHttpService httpService, IMapper mapper, ISearchSelectorBuilder searchSelectorBuilder)
        {
            ConditionalValidationConditionsBuilder = conditionalValidationConditionsBuilder;
            EntityStateUpdater = entityStateUpdater;
            EntityUpdater = entityUpdater;
            FieldsCollectionBuilder = fieldsCollectionBuilder;
            GetItemFilterBuilder = getItemFilterBuilder;
            HttpService = httpService;
            Mapper = mapper;
            SearchSelectorBuilder = searchSelectorBuilder;
        }

        public IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        public IEntityStateUpdater EntityStateUpdater { get; }
        public IEntityUpdater EntityUpdater { get; }
        public IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        public IGetItemFilterBuilder GetItemFilterBuilder { get; }
        public IHttpService HttpService { get; }
        public IMapper Mapper { get; }
        public ISearchSelectorBuilder SearchSelectorBuilder { get; }
    }
}
