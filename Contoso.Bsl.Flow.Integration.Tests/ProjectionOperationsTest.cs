using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests
{
    public class ProjectionOperationsTest
    {
        public ProjectionOperationsTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        #region Tests
        [Fact]
        public void Get_students_with_filtered_inlude_no_filter_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameter
                (
                    new GreaterThanBinaryOperatorParameter
                    (
                        new CountOperatorParameter
                        (
                            new MemberSelectorOperatorParameter("Enrollments", new ParameterOperatorParameter("f"))
                        ),
                        new ConstantOperatorParameter(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        {
                            MemberName = "Enrollments"
                        }
                    }
                )
            );

            Assert.True(students.First().Enrollments.Count > 0);
        }

        [Fact]
        public void Get_students_no_filtered_inlude_no_filter_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameter
                (
                    new GreaterThanBinaryOperatorParameter
                    (
                        new CountOperatorParameter
                        (
                            new MemberSelectorOperatorParameter("Enrollments", new ParameterOperatorParameter("f"))
                        ),
                        new ConstantOperatorParameter(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                null
            );

            Assert.Null(students.First().Enrollments);
        }

        [Fact]
        public void Get_students_with_filtered_inlude_with_filter_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameter
                (
                    new GreaterThanBinaryOperatorParameter
                    (
                        new CountOperatorParameter
                        (
                            new MemberSelectorOperatorParameter("Enrollments", new ParameterOperatorParameter("f"))
                        ),
                        new ConstantOperatorParameter(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        (
                            "enrollments",
                            new SelectExpandItemFilterParameters
                            (
                                new FilterLambdaOperatorParameter
                                (
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new MemberSelectorOperatorParameter("enrollmentID", new ParameterOperatorParameter("a")),
                                        new ConstantOperatorParameter(-1)
                                    ),
                                    typeof(EnrollmentModel),
                                    "a"
                                )
                            ),
                            null,
                            null,
                            null
                        )
                    }
                )
            );

            Assert.False(students.First().Enrollments.Any());
        }

        [Fact]
        public void Get_students_with_filtered_inlude_no_filter_sorted_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameter
                (
                    new GreaterThanBinaryOperatorParameter
                    (
                        new CountOperatorParameter
                        (
                            new MemberSelectorOperatorParameter("Enrollments", new ParameterOperatorParameter("f"))
                        ),
                        new ConstantOperatorParameter(0)
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        (
                            "enrollments",
                            new SelectExpandItemFilterParameters
                            (
                                new FilterLambdaOperatorParameter
                                (
                                    new GreaterThanBinaryOperatorParameter
                                    (
                                        new MemberSelectorOperatorParameter("enrollmentID", new ParameterOperatorParameter("a")),
                                        new ConstantOperatorParameter(0)
                                    ),
                                    typeof(EnrollmentModel),
                                    "a"
                                )
                            ),
                            new SelectExpandItemQueryFunctionParameters
                            (
                                new SortCollectionParameters
                                (
                                    new List<SortDescriptionParameters>
                                    {
                                        new SortDescriptionParameters("Grade", ListSortDirection.Ascending)
                                    },
                                    null,
                                    null
                                )
                            ),
                            null,
                            null
                        )
                    }
                )
            );

            Assert.True(students.First().Enrollments.Count > 0);
            Assert.True
            (
                string.Compare
                (
                    students.First().Enrollments.First().GradeLetter,
                    students.Skip(1).First().Enrollments.First().GradeLetter
                ) <= 0
            );
        }

        [Fact]
        public void Get_students_with_filtered_inlude_no_filter_sort_skip_and_take_select_expand_definition()
        {
            ICollection<StudentModel> students = ProjectionOperations<StudentModel, Student>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameter
                (
                    new AndBinaryOperatorParameter
                    (
                        new EqualsBinaryOperatorParameter
                        (
                            new MemberSelectorOperatorParameter("FirstName", new ParameterOperatorParameter("f")),
                            new ConstantOperatorParameter("Carson")
                        ),
                        new EqualsBinaryOperatorParameter
                        (
                            new MemberSelectorOperatorParameter("LastName", new ParameterOperatorParameter("f")),
                            new ConstantOperatorParameter("Alexander")
                        )
                    ),
                    typeof(StudentModel),
                    "f"
                ),
                null,
                new SelectExpandDefinitionParameters
                (
                    null,
                    new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters
                        (
                            "enrollments",
                            new SelectExpandItemFilterParameters
                            (
                                new FilterLambdaOperatorParameter
                                (
                                    new GreaterThanBinaryOperatorParameter
                                    (
                                        new MemberSelectorOperatorParameter("enrollmentID", new ParameterOperatorParameter("a")),
                                        new ConstantOperatorParameter(0)
                                    ),
                                    typeof(EnrollmentModel),
                                    "a"
                                )
                            ),
                            new SelectExpandItemQueryFunctionParameters
                            (
                                new SortCollectionParameters
                                (
                                    new List<SortDescriptionParameters>
                                    {
                                        new SortDescriptionParameters("Grade", ListSortDirection.Descending)
                                    },
                                    1,
                                    2
                                )
                            ),
                            null,
                            null
                        )
                    }
                )
            );

            Assert.Single(students);
            Assert.Equal(2, students.First().Enrollments.Count);
            Assert.Equal("A", students.First().Enrollments.Last().GradeLetter);
        }

        [Fact]
        public void Get_enrollments_filtered_by_grade_letter()
        {
            ICollection<EnrollmentModel> enrollments = ProjectionOperations<EnrollmentModel, Enrollment>.GetItems
            (
                serviceProvider.GetRequiredService<ISchoolRepository>(),
                serviceProvider.GetRequiredService<IMapper>(),
                new FilterLambdaOperatorParameter
                (
                    new EqualsBinaryOperatorParameter
                    (
                        new MemberSelectorOperatorParameter("GradeLetter", new ParameterOperatorParameter("f")),
                        new ConstantOperatorParameter("A")
                    ),
                    typeof(EnrollmentModel),
                    "f"
                ),
                null,
                null
            );

            Assert.Single(enrollments);
        }
        #endregion Tests

        #region Helpers
        static MapperConfiguration MapperConfiguration;
        private void Initialize()
        {
            if (MapperConfiguration == null)
            {
                MapperConfiguration = new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();

            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=SchoolContext1;ConnectRetryCount=0"
                    )
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .BuildServiceProvider();

            SchoolContext context = serviceProvider.GetRequiredService<SchoolContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }
        #endregion Helpers

        #region Seed DB
        private static async Task Seed_Database(ISchoolRepository repository)
        {
            if ((await repository.CountAsync<StudentModel, Student>()) > 0)
                return;//database has been seeded

            InstructorModel[] instructors = new InstructorModel[]
            {
                new InstructorModel { FirstName = "Roger",   LastName = "Zheng", HireDate = DateTime.Parse("2004-02-12"), EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new InstructorModel { FirstName = "Kim", LastName = "Abercrombie", HireDate = DateTime.Parse("1995-03-11"), EntityState = LogicBuilder.Domain.EntityStateType.Added},
                new InstructorModel { FirstName = "Fadi", LastName = "Fakhouri", HireDate = DateTime.Parse("2002-07-06"), OfficeAssignment = new OfficeAssignmentModel { Location = "Smith 17" }, EntityState = LogicBuilder.Domain.EntityStateType.Added},
                new InstructorModel { FirstName = "Roger", LastName = "Harui", HireDate = DateTime.Parse("1998-07-01"), OfficeAssignment = new OfficeAssignmentModel { Location = "Gowan 27" }, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new InstructorModel { FirstName = "Candace", LastName = "Kapoor", HireDate = DateTime.Parse("2001-01-15"), OfficeAssignment = new OfficeAssignmentModel { Location = "Thompson 304" }, EntityState = LogicBuilder.Domain.EntityStateType.Added }
            };
            await repository.SaveGraphsAsync<InstructorModel, Instructor>(instructors);

            DepartmentModel[] departments = new DepartmentModel[]
            {
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Kim" && i.LastName == "Abercrombie").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new CourseModel {CourseID = 2021, Title = "Composition",    Credits = 3},
                        new CourseModel {CourseID = 2042, Title = "Literature",     Credits = 4}
                    }
                },
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "Mathematics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Fadi" && i.LastName == "Fakhouri").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new CourseModel {CourseID = 1045, Title = "Calculus",       Credits = 4},
                        new CourseModel {CourseID = 3141, Title = "Trigonometry",   Credits = 4}
                    }
                },
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Roger" && i.LastName == "Harui").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new CourseModel {CourseID = 1050, Title = "Chemistry",      Credits = 3}
                    }
                },
                new DepartmentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    Name = "Economics",
                    Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.FirstName == "Candace" && i.LastName == "Kapoor").ID,
                    Courses =  new HashSet<CourseModel>
                    {
                        new CourseModel {CourseID = 4022, Title = "Microeconomics", Credits = 3},
                        new CourseModel {CourseID = 4041, Title = "Macroeconomics", Credits = 3 }
                    }
                }
            };
            await repository.SaveGraphsAsync<DepartmentModel, Department>(departments);

            IEnumerable<CourseModel> courses = departments.SelectMany(d => d.Courses);
            CourseAssignmentModel[] courseInstructors = new CourseAssignmentModel[]
            {
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                new CourseAssignmentModel {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
            };
            await repository.SaveGraphsAsync<CourseAssignmentModel, CourseAssignment>(courseInstructors);

            StudentModel[] students = new StudentModel[]
            {
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.A
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.C
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                        },
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        },
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState =  LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01")
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01")
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Tom",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = 1045,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Billie",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.Parse("2010-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = 1050,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                },
                new StudentModel
                {
                    EntityState = LogicBuilder.Domain.EntityStateType.Added,
                    FirstName = "Jackson",
                    LastName = "Spratt",
                    EnrollmentDate = DateTime.Parse("2017-09-01"),
                    Enrollments = new HashSet<EnrollmentModel>
                    {
                        new EnrollmentModel
                        {
                            CourseID = 2021,
                            Grade = Contoso.Domain.Entities.Grade.B
                        }
                    }
                }
            };

            await repository.SaveGraphsAsync<StudentModel, Student>(students);
        }
        #endregion Seed DB
    }
}
