using AutoMapper;

namespace Contoso.XPlatform.Services
{
    public interface IContextProvider
    {
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
