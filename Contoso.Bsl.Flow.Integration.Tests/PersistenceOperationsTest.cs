using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Utils;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests
{
    public class PersistenceOperationsTest
    {
        public PersistenceOperationsTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private static readonly string parameterName = "$it";
        #endregion Fields

        #region Tests
        [Fact]
        public void Add_A_Single_Entity()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Roger Milla")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    PersistenceOperations<StudentModel, Student>.Save
                    (
                        repository, 
                        new StudentModel
                        {
                            EntityState = LogicBuilder.Domain.EntityStateType.Added,
                            EnrollmentDate = new DateTime(2021, 2, 8),
                            Enrollments = new List<EnrollmentModel>
                            {
                                new EnrollmentModel { CourseID = 1050, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4022, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4041, Grade = Domain.Entities.Grade.A }
                            },
                            FirstName = "Roger",
                            LastName = "Milla"
                        }
                    );
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Empty(returnValue.Enrollments);
                },
                "$it => ($it.FullName == \"Roger Milla\")"
            );
        }

        [Fact]
        public void Add_An_Object_Graph()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Roger Milla")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    PersistenceOperations<StudentModel, Student>.SaveGraph
                    (
                        repository,
                        new StudentModel
                        {
                            EntityState = LogicBuilder.Domain.EntityStateType.Added,
                            EnrollmentDate = new DateTime(2021, 2, 8),
                            Enrollments = new List<EnrollmentModel>
                            {
                                new EnrollmentModel { CourseID = 1050, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4022, Grade = Domain.Entities.Grade.A },
                                new EnrollmentModel { CourseID = 4041, Grade = Domain.Entities.Grade.A }
                            },
                            FirstName = "Roger",
                            LastName = "Milla"
                        }
                    );
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Equal(3, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Roger Milla\")"
            );
        }

        [Fact]
        public void Delete_A_Single_Entity_Using_Save()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                null,
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Deleted;
                    PersistenceOperations<StudentModel, Student>.Save(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Null(returnValue);
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Delete_A_Single_Entity_Using_Delete()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                null,
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    IExpressionParameter expressionParameter = GetFilterParameter<StudentModel>(bodyParameter, parameterName);
                    Expression<Func<StudentModel, bool>> expression = ProjectionOperations<StudentModel, Student>.GetFilter
                    (
                        serviceProvider.GetRequiredService<IMapper>().MapToOperator(expressionParameter)
                    );

                    PersistenceOperations<StudentModel, Student>.Delete(repository, expression);
                },
                returnValue =>
                {
                    Assert.Null(returnValue);
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Update_A_Single_Entity()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                null,
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) => 
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
                    PersistenceOperations<StudentModel, Student>.Save(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Add_An_Entry_To_A_Child_Collection()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    studentModel.Enrollments.Add
                    (
                        new EnrollmentModel
                        {
                            CourseID = 3141,
                            Grade = Domain.Entities.Grade.B,
                            EntityState = LogicBuilder.Domain.EntityStateType.Added
                        }
                    );
                    
                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;

                    PersistenceOperations<StudentModel, Student>.SaveGraph(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Equal(Domain.Entities.Grade.B, returnValue.Enrollments.Single(e => e.CourseID == 3141).Grade);
                    Assert.Equal(4, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Update_An_Entry_In_A_Child_Collection()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    var enrollment = studentModel.Enrollments.Single(e => e.CourseID == 1050);
                    enrollment.Grade = Domain.Entities.Grade.B;

                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
                    enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;

                    PersistenceOperations<StudentModel, Student>.SaveGraph(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Equal(Domain.Entities.Grade.B, returnValue.Enrollments.Single(e => e.CourseID == 1050).Grade);
                    Assert.Equal(3, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }

        [Fact]
        public void Delete_An_Entry_From_A_Child_Collection()
        {
            //arrange
            var bodyParameter = new EqualsBinaryOperatorParameters
            (
                new MemberSelectorOperatorParameters("FullName", new ParameterOperatorParameters(parameterName)),
                new ConstantOperatorParameters("Carson Alexander")
            );

            //act
            DoTest<StudentModel, Student>
            (
                bodyParameter,
                null,
                new SelectExpandDefinitionParameters
                {
                    ExpandedItems = new List<SelectExpandItemParameters>
                    {
                        new SelectExpandItemParameters { MemberName = "Enrollments" }
                    }
                },
                parameterName,
                (StudentModel studentModel, ISchoolRepository repository) =>
                {
                    studentModel.EnrollmentDate = new DateTime(2021, 2, 8);
                    var enrollment = studentModel.Enrollments.Single(e => e.CourseID == 1050);
                    enrollment.Grade = Domain.Entities.Grade.B;

                    studentModel.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
                    enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Deleted;

                    PersistenceOperations<StudentModel, Student>.SaveGraph(repository, studentModel);
                },
                returnValue =>
                {
                    Assert.Equal(new DateTime(2021, 2, 8), returnValue.EnrollmentDate);
                    Assert.Null(returnValue.Enrollments.SingleOrDefault(e => e.CourseID == 1050));
                    Assert.Equal(2, returnValue.Enrollments.Count());
                },
                "$it => ($it.FullName == \"Carson Alexander\")"
            );
        }
        #endregion Tests

        #region Helpers
        private IExpressionParameter GetFilterParameter<T>(IExpressionParameter selectorBody, string parameterName = "$it")
            => new FilterLambdaOperatorParameters
            (
                selectorBody,
                typeof(T),
                parameterName
            );

        void DoTest<TModel, TData>(IExpressionParameter bodyParameter,
            IExpressionParameter queryFunc,
            SelectExpandDefinitionParameters expansion,
            string parameterName, 
            Action<TModel, ISchoolRepository> update, 
            Action<TModel> assert, 
            string expectedExpressionString) where TModel : LogicBuilder.Domain.BaseModel where TData : LogicBuilder.Data.BaseData
        {
            //arrange
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IExpressionParameter expressionParameter = GetFilterParameter<TModel>(bodyParameter, parameterName);

            TestExpressionString();
            TestReturnValue();

            void TestReturnValue()
            {
                //act
                TModel returnValue = ProjectionOperations<TModel, TData>.Get
                (
                    repository,
                    mapper,
                    expressionParameter,
                    queryFunc,
                    expansion
                );

                update(returnValue, repository);

                returnValue = ProjectionOperations<TModel, TData>.Get
                (
                    repository,
                    mapper,
                    expressionParameter,
                    queryFunc,
                    expansion
                );

                //assert
                assert(returnValue);
            }

            void TestExpressionString()
            {
                //act
                Expression<Func<TModel, bool>> expression = ProjectionOperations<TModel, TData>.GetFilter
                (
                    mapper.MapToOperator(expressionParameter)
                );

                //assert
                if (!string.IsNullOrEmpty(expectedExpressionString))
                {
                    AssertFilterStringIsCorrect(expression, expectedExpressionString);
                }
            }
        }

        private void AssertFilterStringIsCorrect(Expression expression, string expected)
        {
            AssertStringIsCorrect(ExpressionStringBuilder.ToString(expression));

            void AssertStringIsCorrect(string resultExpression)
                => Assert.True
                (
                    expected == resultExpression,
                    $"Expected expression '{expected}' but the deserializer produced '{resultExpression}'"
                );
        }

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
                        @"Server=(localdb)\mssqllocaldb;Database=SchoolContext4;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
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
            context.Database.EnsureDeleted();
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
