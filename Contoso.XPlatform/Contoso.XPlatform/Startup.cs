using AutoMapper;
using Contoso.AutoMapperProfiles;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Rules;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.XPlatform
{
    public class Startup
    {
        public static void Init(Action<IServiceCollection> nativeConfigurationServices)
        {
            var services = new ServiceCollection();

            nativeConfigurationServices(services);
            ConfigureServices(services);
            App.ServiceCollection = services;
            App.ServiceProvider = App.ServiceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(SchoolProfile).Assembly);
                        cfg.AllowNullCollections = true;
                    })
                )
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpService>()
                .AddSingleton<ISearchSelectorBuilder, SearchSelectorBuilder>()
                .AddSingleton<IGetItemFilterBuilder, GetItemFilterBuilder>()
                .AddSingleton<IFlowManager, FlowManager>()
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<ScreenData, ScreenData>()
                .AddSingleton<IRulesLoader, RulesLoader>()
                .AddSingleton<IDialogFunctions, DialogFunctions>()
                .AddSingleton<IActions, Actions>()
                .AddTransient<IMapper>
                (
                    sp => new Mapper
                    (
                        sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), 
                        sp.GetService
                    )
                )
                .AddTransient<MainPageViewModel, MainPageViewModel>()
                .AddTransient<EditFormViewModel, EditFormViewModel>()
                .AddTransient<SearchPageViewModel, SearchPageViewModel>()
                .AddTransient<ListPageViewModel, ListPageViewModel>()
                .AddTransient<TextPageViewModel, TextPageViewModel>()
                .AddTransient<ExtendedSplashViewModel, ExtendedSplashViewModel>();
        }
    }
}
