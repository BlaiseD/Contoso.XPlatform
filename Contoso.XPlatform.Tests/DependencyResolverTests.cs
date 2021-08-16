using AutoMapper;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class DependencyResolverTests
    {
        public DependencyResolverTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void CanResolveEditFormViewModel()
        {
            EditFormViewModel editFormViewModel = serviceProvider.GetRequiredService<EditFormViewModel>();
            Assert.NotNull(editFormViewModel);
        }

        [Fact]
        public void CanResolveEditFormViewModelWithNonGenericConstructor()
        {
            EditFormViewModel editFormViewModel = (EditFormViewModel)serviceProvider.GetRequiredService(typeof(EditFormViewModel));
            Assert.NotNull(editFormViewModel);
        }

        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddSingleton<UiNotificationService, UiNotificationService>()
                .AddSingleton<IFieldsCollectionBuilder, FieldsCollectionBuilder>()
                .AddSingleton<IConditionalValidationConditionsBuilder, ConditionalValidationConditionsBuilder>()
                .AddHttpClient()
                .AddSingleton<IHttpService, HttpServiceMock>()
                .AddTransient<EditFormViewModel, EditFormViewModel>()
                .BuildServiceProvider();
        }
    }
}
