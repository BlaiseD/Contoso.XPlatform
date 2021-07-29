using AutoMapper;
using Contoso.Domain.Entities;
using Contoso.XPlatform.Services;
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
        public void MapInstructorModelToIValidatableListWithInlineOfficeAssignment()
        {
            //arrange
            InstructorModel inststructor = new InstructorModel
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses = new List<CourseAssignmentModel>
                {
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IValidatable> properties = new ObservableCollection<IValidatable>();
            new FieldsCollectionHelper
            (
                Descriptors.InstructorFormWithInlineOfficeAssignment, 
                properties, 
                new UiNotificationService(),
                new HttpServiceMock(),
                serviceProvider.GetRequiredService<IMapper>()
            ).CreateFieldsCollection();

            //act
            properties.UpdateValidatables
            (
                inststructor,
                Descriptors.InstructorFormWithInlineOfficeAssignment.FieldSettings,
                serviceProvider.GetRequiredService<IMapper>()
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", propertiesDictionary["OfficeAssignment.Location"]);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]).First().CourseTitle);
        }

        [Fact]
        public void MapInstructorModelToIValidatableListWithPopupOfficeAssignment()
        {
            //arrange
            InstructorModel instructor = new InstructorModel
            {
                ID = 3,
                FirstName = "John",
                LastName = "Smith",
                HireDate = new DateTime(2021, 5, 20),
                OfficeAssignment = new OfficeAssignmentModel
                {
                    Location = "Location1"
                },
                Courses = new List<CourseAssignmentModel>
                {
                    new CourseAssignmentModel
                    {
                        CourseID = 1,
                        InstructorID = 2,
                        CourseTitle = "Chemistry"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 2,
                        InstructorID = 3,
                        CourseTitle = "Physics"
                    },
                    new CourseAssignmentModel
                    {
                        CourseID = 3,
                        InstructorID = 4,
                        CourseTitle = "Mathematics"
                    }
                }
            };
            ObservableCollection<IValidatable> properties = new ObservableCollection<IValidatable>();
            new FieldsCollectionHelper
            (
                Descriptors.InstructorFormWithPopupOfficeAssignment,
                properties,
                new UiNotificationService(),
                new HttpServiceMock(),
                serviceProvider.GetRequiredService<IMapper>()
            ).CreateFieldsCollection();

            //act
            properties.UpdateValidatables
            (
                instructor,
                Descriptors.InstructorFormWithPopupOfficeAssignment.FieldSettings,
                serviceProvider.GetRequiredService<IMapper>()
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);
            Assert.Equal(3, propertiesDictionary["ID"]);
            Assert.Equal("John", propertiesDictionary["FirstName"]);
            Assert.Equal("Smith", propertiesDictionary["LastName"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["HireDate"]);
            Assert.Equal("Location1", ((OfficeAssignmentModel)propertiesDictionary["OfficeAssignment"]).Location);
            Assert.Equal("Chemistry", ((IEnumerable<CourseAssignmentModel>)propertiesDictionary["Courses"]).First().CourseTitle);
        }

        [Fact]
        public void MapDepartmentModelToIValidatableList()
        {
            //arrange
            DepartmentModel department = new DepartmentModel
            {
                DepartmentID = 1,
                Name = "Mathematics",
                Budget = 100000m,
                StartDate = new DateTime(2021, 5, 20),
                InstructorID = 1,
                Courses = new List<CourseModel>
                {
                    new CourseModel
                    {
                        CourseID = 1,
                        Credits = 3,
                        Title = "Trigonometry"
                    },
                    new CourseModel
                    {
                        CourseID = 2,
                        Credits = 4,
                        Title = "Physics"
                    },
                    new CourseModel
                    {
                        CourseID = 3,
                        Credits = 5,
                        Title = "Calculus"
                    }
                }
            };
            ObservableCollection<IValidatable> properties = new ObservableCollection<IValidatable>();
            new FieldsCollectionHelper
            (
                Descriptors.DepartmentForm,
                properties,
                new UiNotificationService(),
                new HttpServiceMock(),
                serviceProvider.GetRequiredService<IMapper>()
            ).CreateFieldsCollection();

            //act
            properties.UpdateValidatables
            (
                department,
                Descriptors.DepartmentForm.FieldSettings,
                serviceProvider.GetRequiredService<IMapper>()
            );

            //assert
            IDictionary<string, object> propertiesDictionary = properties.ToDictionary(property => property.Name, property => property.Value);

            Assert.Equal(1, propertiesDictionary["DepartmentID"]);
            Assert.Equal("Mathematics", propertiesDictionary["Name"]);
            Assert.Equal(100000m, propertiesDictionary["Budget"]);
            Assert.Equal(new DateTime(2021, 5, 20), propertiesDictionary["StartDate"]);
            Assert.Equal(1, propertiesDictionary["InstructorID"]);
            Assert.Equal("Trigonometry", ((IEnumerable<CourseModel>)propertiesDictionary["Courses"]).First().Title);
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
