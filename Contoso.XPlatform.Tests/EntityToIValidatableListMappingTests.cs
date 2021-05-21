using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public void MappInstructorModelToIValidatableList()
        {
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

            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            Dictionary<string, object> instructorDictionary =  mapper.Map<Dictionary<string, object>>(inststructor);

            ObservableCollection<IValidatable> properties = new ObservableCollection<IValidatable>();
            FieldsCollectionHelper fieldsCollectionHelper = new FieldsCollectionHelper(properties, new UiNotificationService(), null);
            fieldsCollectionHelper.CreateFieldsCollection(Descriptors.InstructorForm.FieldSettings, Descriptors.InstructorForm.ValidationMessages);
            properties.UpdateValidatables
            (
                Descriptors.InstructorForm.FieldSettings,
                instructorDictionary,
                mapper
            );

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
