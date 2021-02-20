using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Flow.Cache;
using Contoso.Bsl.Flow.Requests;
using Contoso.Bsl.Flow.Responses;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.RulesDirector;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests.Rules
{
    public class SaveStudentTest
    {
        public SaveStudentTest(Xunit.Abstractions.ITestOutputHelper output)
        {
            this.output = output;
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private readonly Xunit.Abstractions.ITestOutputHelper output;
        #endregion Fields

        #region Tests
        [Fact]
        public void SaveStudentRequestWithEnrollments()
        {
            //arrange
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>(s => s.FullName == "Carson Alexander").Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveStudentRequest { Student = student };


            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savestudent");
            stopWatch.Stop();
            this.output.WriteLine("CheckInitializtion Rules Execution = {0}", stopWatch.Elapsed.TotalMilliseconds);


            flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>(s => s.FullName == "First Alexander").Result.Single();
            student.FirstName = "Second";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveStudentRequest { Student = student };
            stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("savestudent");
            stopWatch.Stop();
            this.output.WriteLine("CheckInitializtion2 Rules Execution = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal("Second", ((SaveStudentResponse)flowManager.FlowDataCache.Response).Student.FirstName);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollmentsWithoutRules()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            ISchoolRepository schoolRepository = serviceProvider.GetRequiredService<ISchoolRepository>();
            var student = schoolRepository.GetAsync<StudentModel, Student>(s => s.FullName == "Carson Alexander").Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            flowManager.FlowDataCache.Request = new SaveStudentRequest { Student = student };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();

            flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"] = ((Contoso.Bsl.Flow.Requests.SaveStudentRequest)flowManager.FlowDataCache.Request).Student;
            flowManager.FlowDataCache.Response = Contoso.Bsl.Flow.ResponseHelper<Contoso.Bsl.Flow.Responses.SaveStudentResponse>.CreateResponse();
            flowManager.FlowDataCache.Response.Success = Contoso.Bsl.Flow.PersistenceOperations<Contoso.Domain.Entities.StudentModel, Contoso.Data.Entities.Student>.SaveGraph(flowManager.SchoolRepository, (Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]);
            ((Contoso.Bsl.Flow.Responses.SaveStudentResponse)flowManager.FlowDataCache.Response).Student = Contoso.Bsl.Flow.ProjectionOperations<Contoso.Domain.Entities.StudentModel, Contoso.Data.Entities.Student>.Get
            (
                flowManager.SchoolRepository, 
                flowManager.Mapper, 
                new Contoso.Parameters.Expressions.FilterLambdaOperatorParameter
                (
                    new Contoso.Parameters.Expressions.EqualsBinaryOperatorParameter
                    (
                        new Contoso.Parameters.Expressions.MemberSelectorOperatorParameter
                        (
                            LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_ID", flowManager.Director), 
                            new Contoso.Parameters.Expressions.ParameterOperatorParameter
                            (
                                LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_F", flowManager.Director)
                            )
                        ), 
                        new Contoso.Parameters.Expressions.ConstantOperatorParameter
                        (
                            ((Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]).ID, 
                            Contoso.Bsl.Flow.TypeHelper.GetType
                            (
                                LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource
                                (
                                    "savestudent_SYSTEM", 
                                    flowManager.Director
                                )
                            )
                        )
                    ), 
                    Contoso.Bsl.Flow.TypeHelper.GetType
                    (
                        LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_CCVCP", flowManager.Director)
                    ), 
                    LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_F", flowManager.Director)
                ), 
                null, 
                new Contoso.Parameters.Expansions.SelectExpandDefinitionParameters
                (
                    new System.Collections.Generic.List<string>(new string[0]), 
                    new System.Collections.Generic.List<Contoso.Parameters.Expansions.SelectExpandItemParameters>
                    (
                        new Contoso.Parameters.Expansions.SelectExpandItemParameters[] 
                        { 
                            new Contoso.Parameters.Expansions.SelectExpandItemParameters(LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_ENROLL", flowManager.Director), null, null, null, null) 
                        }
                    )
                )
            );

            flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"] = Contoso.Bsl.Flow.ProjectionOperations<Contoso.Domain.Entities.StudentModel, Contoso.Data.Entities.Student>.Get
            (
                flowManager.SchoolRepository, 
                flowManager.Mapper, 
                new Contoso.Parameters.Expressions.FilterLambdaOperatorParameter
                (
                    new Contoso.Parameters.Expressions.EqualsBinaryOperatorParameter
                    (
                        new Contoso.Parameters.Expressions.MemberSelectorOperatorParameter
                        (
                            LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_ID", flowManager.Director), 
                            new Contoso.Parameters.Expressions.ParameterOperatorParameter
                            (
                                LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_F", flowManager.Director)
                            )
                        ), 
                        new Contoso.Parameters.Expressions.ConstantOperatorParameter
                        (
                            ((Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]).ID, 
                            Contoso.Bsl.Flow.TypeHelper.GetType
                            (
                                LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_SYSTEM", flowManager.Director)
                            )
                        )
                    ), 
                    Contoso.Bsl.Flow.TypeHelper.GetType
                    (
                        LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_CCVCP", flowManager.Director)
                    ), 
                    LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_F", flowManager.Director)
                ), 
                null, 
                new Contoso.Parameters.Expansions.SelectExpandDefinitionParameters
                (
                    new System.Collections.Generic.List<string>(new string[0]), 
                    new System.Collections.Generic.List<Contoso.Parameters.Expansions.SelectExpandItemParameters>
                    (
                        new Contoso.Parameters.Expansions.SelectExpandItemParameters[] 
                        { 
                            new Contoso.Parameters.Expansions.SelectExpandItemParameters
                            (
                                LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_ENROLL", flowManager.Director), 
                                null, 
                                null, 
                                null, 
                                null
                            ) 
                        }
                    )
                )
            );

            flowManager.FlowDataCache.Items["Iteration_Index"] = 0;
            flowManager.CustomActions.WriteToLog
            (
                string.Format
                (
                    LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_EAIA", flowManager.Director), 
                    new System.Collections.ObjectModel.Collection<object>
                    (
                        new object[] 
                        { 
                            ((Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]).Enrollments.Count, 
                            (int)flowManager.FlowDataCache.Items["Iteration_Index"] 
                        }
                    ).ToArray()
                )
            );

            while ((int)flowManager.FlowDataCache.Items["Iteration_Index"] < ((Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]).Enrollments.Count)
            {
                flowManager.FlowDataCache.Items["Contoso.Domain.Entities.EnrollmentModel"] = Contoso.Bsl.Flow.Flow.GenericsHelpers<Contoso.Domain.Entities.EnrollmentModel>.ItemByIndex(((Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]).Enrollments, (int)flowManager.FlowDataCache.Items["Iteration_Index"]);
                flowManager.FlowDataCache.Items["Iteration_Index"] = (int)flowManager.FlowDataCache.Items["Iteration_Index"] + 1;
                flowManager.CustomActions.WriteToLog
                (
                    string.Format
                    (
                        LogicBuilder.RulesDirector.ResourcesHelper<string>.GetResource("savestudent_SIIEIA", flowManager.Director), 
                        new System.Collections.ObjectModel.Collection<object>
                        (
                            new object[] 
                            { 
                                ((Contoso.Domain.Entities.StudentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.StudentModel"]).ID, 
                                ((Contoso.Domain.Entities.EnrollmentModel)flowManager.FlowDataCache.Items["Contoso.Domain.Entities.EnrollmentModel"]).CourseTitle 
                            }
                        ).ToArray()
                    )
                );
            }

            stopWatch.Stop();
            this.output.WriteLine("WithoutRules Execution = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal("First", ((SaveStudentResponse)flowManager.FlowDataCache.Response).Student.FirstName);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollmentsWithoutRulesUsingExpressiionsFromCode()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            ISchoolRepository schoolRepository = serviceProvider.GetRequiredService<ISchoolRepository>();
            var student = schoolRepository.GetAsync<StudentModel, Student>(s => s.FullName == "Carson Alexander").Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            SaveStudentRequest saveStudentRequest = new SaveStudentRequest { Student = student };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            StudentModel studentModel = saveStudentRequest.Student;
            SaveStudentResponse saveStudentResponse = new SaveStudentResponse();
            saveStudentResponse.Success = schoolRepository.SaveGraphAsync<StudentModel, Student>(studentModel).Result;
            saveStudentResponse.Student = schoolRepository.GetAsync<StudentModel, Student>
            (
                f => f.ID == studentModel.ID,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.SingleOrDefault();

            studentModel = schoolRepository.GetAsync<StudentModel, Student>
            (
                f => f.ID == studentModel.ID,
                null,
                new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.SingleOrDefault();

            int Iteration_Index = 0;

            flowManager.CustomActions.WriteToLog
            (
                string.Format
                (
                    "EnrollmentCount: {0}. Index: {1}",
                    new object[]
                    {
                        studentModel.Enrollments.Count,
                        Iteration_Index
                    }
                )
            );

            EnrollmentModel enrollmentModel = null;
            while (Iteration_Index < studentModel.Enrollments.Count)
            {
                enrollmentModel = studentModel.Enrollments.ElementAt(Iteration_Index);
                Iteration_Index = Iteration_Index + 1;
                flowManager.CustomActions.WriteToLog
                (
                    string.Format
                    (
                        "Student Id:{0} is enrolled in {1}.",
                        new object[]
                        {
                            studentModel.ID,
                            enrollmentModel.CourseTitle
                        }
                    )
                );
            }

            stopWatch.Stop();
            this.output.WriteLine("WithoutRulesUsingExpressiionsInCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
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
                        @"Server=(localdb)\mssqllocaldb;Database=SaveStudentTest;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Transient
                )
                .AddLogging
                (
                    loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders();
                        loggingBuilder.Services.AddSingleton<ILoggerProvider>
                        (
                            serviceProvider => new XUnitLoggerProvider(this.output)
                        );
                        loggingBuilder.AddFilter<XUnitLoggerProvider>("*", LogLevel.None);
                        loggingBuilder.AddFilter<XUnitLoggerProvider>("Contoso.Bsl.Flow", LogLevel.Trace);
                    }
                )
                .AddTransient<ISchoolStore, SchoolStore>()
                .AddTransient<ISchoolRepository, SchoolRepository>()
                .AddSingleton<AutoMapper.IConfigurationProvider>
                (
                    MapperConfiguration
                )
                .AddTransient<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
                .AddTransient<IFlowManager, FlowManager>()
                .AddTransient<FlowActivityFactory, FlowActivityFactory>()
                .AddTransient<DirectorFactory, DirectorFactory>()
                .AddTransient<ICustomActions, CustomActions>()
                .AddSingleton<IRulesCache>(sp =>
                {
                    return Bsl.Flow.Rules.RulesService.LoadRules().GetAwaiter().GetResult();
                })
                .BuildServiceProvider();

            ReCreateDataBase(serviceProvider.GetRequiredService<SchoolContext>());
            Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
        }

        private static void ReCreateDataBase(SchoolContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
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
