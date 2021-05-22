using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace Contoso.XPlatform.Tests
{
    public class IValidatableListToEntityMappingTests
    {
        public IValidatableListToEntityMappingTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapIValidatableListModelToInstructor()
        {
            //arrange
            ObservableCollection<IValidatable> properties = new ObservableCollection<IValidatable>();
            new FieldsCollectionHelper
            (
                Descriptors.InstructorForm,
                properties,
                new UiNotificationService(),
                null
            ).CreateFieldsCollection();
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(property => property.Name);
            propertiesDictionary["FirstName"].Value = "John";
            propertiesDictionary["LastName"].Value = "Smith";
            propertiesDictionary["HireDate"].Value = new DateTime(2021, 5, 20);
            propertiesDictionary["OfficeAssignment.Location"].Value = "Location1";

            //act
            InstructorModel instructorModel = (InstructorModel)properties.ToModelObject(typeof(InstructorModel), serviceProvider.GetRequiredService<IMapper>());

            //assert
            Assert.Equal("John", instructorModel.FirstName);
            Assert.Equal("Smith", instructorModel.LastName);
            Assert.Equal(new DateTime(2021, 5, 20), instructorModel.HireDate);
            Assert.Equal("Location1", instructorModel.OfficeAssignment.Location);
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
                .BuildServiceProvider();
        }
    }
}
