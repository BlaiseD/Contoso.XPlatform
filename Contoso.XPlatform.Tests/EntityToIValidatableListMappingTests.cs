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
    public class EntityToIValidatableListMappingTests
    {
        public EntityToIValidatableListMappingTests()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void MapInstructorModelToIValidatableList()
        {
            //arrange
            InstructorModel inststructor = new InstructorModel
            {
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                }
            };
            ObservableCollection<IValidatable> properties = new ObservableCollection<IValidatable>();
            new FieldsCollectionHelper
            (
                Descriptors.InstructorForm, 
                properties, 
                new UiNotificationService(), 
                null
            ).CreateFieldsCollection();

            //act
            properties.UpdateValidatables
            (
                inststructor,
                Descriptors.InstructorForm.FieldSettings,
                serviceProvider.GetRequiredService<IMapper>()
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", propertiesDictionary["OfficeAssignment.Location"]);
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
