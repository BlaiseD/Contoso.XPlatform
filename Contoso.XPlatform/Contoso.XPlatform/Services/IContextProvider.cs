using AutoMapper;

namespace Contoso.XPlatform.Services
{
    public interface IContextProvider
    {
        IConditionalValidationConditionsBuilder ConditionalValidationConditionsBuilder { get; }
        IEntityStateUpdater EntityStateUpdater { get; }
        IEntityUpdater EntityUpdater { get; }
        IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        IGetItemFilterBuilder GetItemFilterBuilder { get; }
        IHttpService HttpService { get; }
        IMapper Mapper { get; }
        ISearchSelectorBuilder SearchSelectorBuilder { get; }
        IPropertiesUpdater PropertiesUpdater { get; }
        UiNotificationService UiNotificationService { get; }
    }
}
