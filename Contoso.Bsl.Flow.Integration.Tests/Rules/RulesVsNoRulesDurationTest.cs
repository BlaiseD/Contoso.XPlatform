using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow.Cache;
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
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests.Rules
{
    public class RulesVsNoRulesDurationTest
    {
        public RulesVsNoRulesDurationTest(Xunit.Abstractions.ITestOutputHelper output)
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
        public void SaveStudentRequestWithEnrollments1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();

            //arrange
            flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            flowManager.FlowDataCache.Request = new SaveStudentRequest { Student = student };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("comparisontest");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid student using rules = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal("First", ((SaveStudentResponse)flowManager.FlowDataCache.Response).Student.FirstName);
            Assert.Equal(Domain.Entities.Grade.A, ((SaveStudentResponse)flowManager.FlowDataCache.Response).Student.Enrollments.First().Grade);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollments2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();

            //arrange
            flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            flowManager.FlowDataCache.Request = new SaveStudentRequest { Student = student };

            //act
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("comparisontest");
            stopWatch.Stop();
            this.output.WriteLine("Saving valid student using rules = {0}", stopWatch.Elapsed.TotalMilliseconds);

            //assert
            Assert.True(flowManager.FlowDataCache.Response.Success);
            Assert.Equal("First", ((SaveStudentResponse)flowManager.FlowDataCache.Response).Student.FirstName);
            Assert.Equal(Domain.Entities.Grade.A, ((SaveStudentResponse)flowManager.FlowDataCache.Response).Student.Enrollments.First().Grade);
        }

        [Fact]
        public void SaveStudentRequestWithEnrollmentsWithoutRules1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            ISchoolRepository schoolRepository = serviceProvider.GetRequiredService<ISchoolRepository>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            SaveStudentRequest saveStudentRequest = new SaveStudentRequest { Student = student };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            StudentModel studentModel = saveStudentRequest.Student;
            SaveStudentResponse saveStudentResponse = new SaveStudentResponse();
            saveStudentResponse.Success = schoolRepository.SaveGraphAsync<StudentModel, Student>(studentModel).Result;

            if (!saveStudentResponse.Success) return;

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

            saveStudentResponse.Student = studentModel;

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

        [Fact]
        public void SaveStudentRequestWithEnrollmentsWithoutRules2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            ISchoolRepository schoolRepository = serviceProvider.GetRequiredService<ISchoolRepository>();
            var student = flowManager.SchoolRepository.GetAsync<StudentModel, Student>
            (
                s => s.FullName == "Carson Alexander",
                selectExpandDefinition: new LogicBuilder.Expressions.Utils.Expansions.SelectExpandDefinition
                {
                    ExpandedItems = new List<LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem>
                    {
                        new LogicBuilder.Expressions.Utils.Expansions.SelectExpandItem { MemberName = "enrollments" }
                    }
                }
            ).Result.Single();
            student.FirstName = "First";
            student.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            student.Enrollments.ToList().ForEach(enrollment =>
            {
                enrollment.Grade = Domain.Entities.Grade.A;
                enrollment.EntityState = LogicBuilder.Domain.EntityStateType.Modified;
            });
            SaveStudentRequest saveStudentRequest = new SaveStudentRequest { Student = student };

            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            StudentModel studentModel = saveStudentRequest.Student;
            SaveStudentResponse saveStudentResponse = new SaveStudentResponse();
            saveStudentResponse.Success = schoolRepository.SaveGraphAsync<StudentModel, Student>(studentModel).Result;

            if (!saveStudentResponse.Success) return;

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

            saveStudentResponse.Student = studentModel;

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

        [Fact]
        public void JustLoopWithRules1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveStudentResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloop");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRules2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveStudentResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloop");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRulesNoBoxing1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveStudentResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloopnoboxing");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithRulesNoBoxing2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            flowManager.FlowDataCache.Response = new SaveStudentResponse();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.Start("justloopnoboxing");
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCode1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Items["Iteration_Index"] = 0;
            while ((int)flowManager.FlowDataCache.Items["Iteration_Index"] < 1000000)
            {
                flowManager.FlowDataCache.Items["Iteration_Index"] = (int)flowManager.FlowDataCache.Items["Iteration_Index"] + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCode2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Items["Iteration_Index"] = 0;
            while ((int)flowManager.FlowDataCache.Items["Iteration_Index"] < 1000000)
            {
                flowManager.FlowDataCache.Items["Iteration_Index"] = (int)flowManager.FlowDataCache.Items["Iteration_Index"] + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCodeNoBoxing1()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Index1 = 0;
            while (flowManager.FlowDataCache.Index1 < 1000000)
            {
                flowManager.FlowDataCache.Index1 = flowManager.FlowDataCache.Index1 + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
        }

        [Fact]
        public void JustLoopWithCodeNoBoxing2()
        {
            IFlowManager flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            flowManager.FlowDataCache.Index1 = 0;
            while (flowManager.FlowDataCache.Index1 < 1000000)
            {
                flowManager.FlowDataCache.Index1 = flowManager.FlowDataCache.Index1 + 1;
            }
            stopWatch.Stop();
            this.output.WriteLine("JustLoopWithCode = {0}", stopWatch.Elapsed.TotalMilliseconds);
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
                        @"Server=(localdb)\mssqllocaldb;Database=RulesVsNoRulesDurationTest;ConnectRetryCount=0"
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
                .AddSingleton<FlowDataCache, FlowDataCache>()
                .AddSingleton<Progress, Progress>()
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
