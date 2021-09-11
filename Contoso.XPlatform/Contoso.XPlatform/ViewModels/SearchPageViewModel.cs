using Contoso.Forms.Configuration.SearchForm;
using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.SearchPage;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels
{
    public class SearchPageViewModel : FlyoutDetailViewModelBase
    {
        private readonly UiNotificationService uiNotificationService;
        private readonly IHttpService httpService;
        private readonly ISearchSelectorBuilder searchSelectorBuilder;
        private readonly IGetItemFilterBuilder getItemFilterBuilder;

        public SearchPageViewModel(UiNotificationService uiNotificationService, IHttpService httpService, ISearchSelectorBuilder searchSelectorBuilder, IGetItemFilterBuilder getItemFilterBuilder)
        {
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
            this.searchSelectorBuilder = searchSelectorBuilder;
            this.getItemFilterBuilder = getItemFilterBuilder;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            SearchPageEntityViewModel = CreateSearchPageListViewModel((ScreenSettings<SearchFormSettingsDescriptor>)screenSettings);
        }

        public SearchPageCollectionViewModelBase SearchPageEntityViewModel { get; set; }

        private SearchPageCollectionViewModelBase CreateSearchPageListViewModel(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            return (SearchPageCollectionViewModelBase)Activator.CreateInstance
            (
                typeof(SearchPageCollectionViewModel<>).MakeGenericType
                (
                    Type.GetType
                    (
                        screenSettings.Settings.ModelType,
                        AssemblyResolver,
                        TypeResolver
                    )
                ),
                new object[]
                {
                    screenSettings,
                    this.uiNotificationService,
                    this.httpService,
                    this.searchSelectorBuilder,
                    this.getItemFilterBuilder
                }
            );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
