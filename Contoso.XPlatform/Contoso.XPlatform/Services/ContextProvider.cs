using AutoMapper;

namespace Contoso.XPlatform.Services
{
    public class ContextProvider : IContextProvider
    {
        public ContextProvider(UiNotificationService uiNotificationService, IConditionalValidationConditionsBuilder conditionalValidationConditionsBuilder, IEntityStateUpdater entityStateUpdater, IEntityUpdater entityUpdater, IGetItemFilterBuilder getItemFilterBuilder, IHttpService httpService, IMapper mapper, ISearchSelectorBuilder searchSelectorBuilder)
        {
            UiNotificationService = uiNotificationService;
            ConditionalValidationConditionsBuilder = conditionalValidationConditionsBuilder;
            EntityStateUpdater = entityStateUpdater;
            EntityUpdater = entityUpdater;
            GetItemFilterBuilder = getItemFilterBuilder;
            HttpService = httpService;
            Mapper = mapper;
            SearchSelectorBuilder = searchSelectorBuilder;

            //passing IContextProvider to FieldsCollectionBuilder will create a circular dependency
            //so creating the instance here instead of using DI.
            FieldsCollectionBuilder = new FieldsCollectionBuilder(this);
        }

        public IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        public IEntityStateUpdater EntityStateUpdater { get; }
        public IEntityUpdater EntityUpdater { get; }
        public IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        public IGetItemFilterBuilder GetItemFilterBuilder { get; }
        public IHttpService HttpService { get; }
        public IMapper Mapper { get; }
        public ISearchSelectorBuilder SearchSelectorBuilder { get; }
        public UiNotificationService UiNotificationService { get; }
    }
}
