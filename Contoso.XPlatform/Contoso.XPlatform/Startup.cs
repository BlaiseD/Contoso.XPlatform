using AutoMapper;
using Contoso.AutoMapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(SchoolProfile).Assembly);
                        cfg.AllowNullCollections = true;
                    })
                )
                .AddTransient<IMapper>
                (
                    sp => new Mapper
                    (
                        sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), 
                        sp.GetService
                    )
                );
        }
    }
}
