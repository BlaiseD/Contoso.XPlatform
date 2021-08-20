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

        public SearchPageViewModel(UiNotificationService uiNotificationService, IHttpService httpService, ISearchSelectorBuilder searchSelectorBuilder)
        {
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
            this.searchSelectorBuilder = searchSelectorBuilder;
        }

        public override void Initialize(ScreenSettingsBase screenSettings)
        {
            SearchPageEntityViewModel = CreateSearchPageListViewModel((ScreenSettings<SearchFormSettingsDescriptor>)screenSettings);
        }

        public SearchPageListViewModelBase SearchPageEntityViewModel { get; set; }

        private SearchPageListViewModelBase CreateSearchPageListViewModel(ScreenSettings<SearchFormSettingsDescriptor> screenSettings)
        {
            return (SearchPageListViewModelBase)Activator.CreateInstance
            (
                typeof(SearchPageListViewModel<>).MakeGenericType
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
                    this.searchSelectorBuilder
                }
            );

            Type TypeResolver(Assembly assembly, string typeName, bool matchCase)
                => assembly.GetType(typeName);

            Assembly AssemblyResolver(AssemblyName assemblyName)
                => typeof(Domain.BaseModelClass).Assembly;
        }
    }
}
