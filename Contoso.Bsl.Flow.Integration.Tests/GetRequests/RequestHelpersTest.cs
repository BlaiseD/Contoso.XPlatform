using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Bsl.Utils;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
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

namespace Contoso.Bsl.Flow.Integration.Tests.GetRequests
{
    public class RequestHelpersTest
    {
        public RequestHelpersTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        #endregion Fields

        [Fact]
        public void Select_Departments_In_Ascending_Order_As_Anonymous_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<DepartmentModel>, IEnumerable<dynamic>>
            (
                GetDepartmentsBodyForLookupModelType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetAnonymousSelect<DepartmentModel, Department>
            (
                new Business.Requests.GetAnonymousDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                    DataType = typeof(Department).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.DropDownList.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(d => d.Name).Select(d => new LookUpsModel() {NumericValue = Convert(d.DepartmentID), Text = d.Name}))");
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public void Select_Departments_In_Ascending_Order_As_DepartmentModel_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<DepartmentModel>, IEnumerable<DepartmentModel>>
            (
                GetDepartmentsBodyForDepartmentModelType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetObjectSelect<DepartmentModel, Department, IEnumerable<DepartmentModel>, IEnumerable<Department>>
            (
                new Business.Requests.GetTypedDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(DepartmentModel).AssemblyQualifiedName,
                    DataType = typeof(Department).AssemblyQualifiedName,
                    ModelReturnType = typeof(IEnumerable<DepartmentModel>).AssemblyQualifiedName,
                    DataReturnType = typeof(IEnumerable<Department>).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.DropDownList.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.OrderBy(d => d.Name).Select(d => new DepartmentModel() {DepartmentID = d.DepartmentID, Name = d.Name}))");
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public void Select_Credits_From_Lookups_Table_In_Descending_Order_As_Anonymous_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<dynamic>>
            (
                GetBodyForAnonymousType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetAnonymousSelect<LookUpsModel, LookUps>
            (
                new Business.Requests.GetAnonymousDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                    DataType = typeof(LookUps).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.DropDownList.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.Where(l => (l.ListName == \"Credits\")).OrderByDescending(l => l.NumericValue).Select(l => new AnonymousType() {NumericValue = l.NumericValue, Text = l.Text}))");
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void Select_Credits_From_Lookups_Table_In_Descending_Order_From_DropDownListRequest_As_Anonymous_Type()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<dynamic>>
            (
                GetBodyForAnonymousType(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetAnonymousSelect
            (
                new Business.Requests.GetAnonymousDropDownListRequest 
                { 
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                    DataType = typeof(LookUps).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.DropDownList.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.Where(l => (l.ListName == \"Credits\")).OrderByDescending(l => l.NumericValue).Select(l => new AnonymousType() {NumericValue = l.NumericValue, Text = l.Text}))");
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void Select_Credits_From_Lookups_Table_In_Descending_Order_As_LookUpsModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<LookUpsModel>>
            (
                GetBodyForLookupsModel(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var response = RequestHelpers.GetLookupSelect<LookUpsModel, LookUps, IEnumerable<LookUpsModel>, IEnumerable<LookUps>>
            (
                new Business.Requests.GetTypedDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                    DataType = typeof(LookUps).AssemblyQualifiedName,
                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result;

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.Where(l => (l.ListName == \"Credits\")).OrderByDescending(l => l.NumericValue).Select(l => new LookUpsModel() {NumericValue = l.NumericValue, Text = l.Text}))");
            Assert.Equal(5, response.DropDownList.Count());
        }

        [Fact]
        public void Select_Credits_From_Lookups_Table_In_Descending_Order_From_DropDownListRequest_As_LookUpsModel()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<LookUpsModel>>
            (
                GetBodyForLookupsModel(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetLookupSelect
            (
                new Business.Requests.GetTypedDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                    DataType = typeof(LookUps).AssemblyQualifiedName,
                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.DropDownList.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.Where(l => (l.ListName == \"Credits\")).OrderByDescending(l => l.NumericValue).Select(l => new LookUpsModel() {NumericValue = l.NumericValue, Text = l.Text}))");
            Assert.Equal(5, list.Count);
        }

        [Fact]
        public void Select_Credits_From_Lookups_Table_In_Descending_Order_As_LookUpsModel_Using_Object_ReturnType()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<LookUpsModel>>
            (
                GetBodyForLookupsModel(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var response = RequestHelpers.GetObjectSelect<LookUpsModel, LookUps, IEnumerable<LookUpsModel>, IEnumerable<LookUps>>
            (
                new Business.Requests.GetTypedDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                    DataType = typeof(LookUps).AssemblyQualifiedName,
                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result;

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.Where(l => (l.ListName == \"Credits\")).OrderByDescending(l => l.NumericValue).Select(l => new LookUpsModel() {NumericValue = l.NumericValue, Text = l.Text}))");
            Assert.Equal(5, response.DropDownList.Count());
        }

        [Fact]
        public void Select_Credits_From_Lookups_Table_In_Descending_Order_From_DropDownListRequest_As_LookUpsModel_Using_Object_ReturnType()
        {
            //arrange
            var selectorLambdaOperatorDescriptor = GetExpressionDescriptor<IQueryable<LookUpsModel>, IEnumerable<LookUpsModel>>
            (
                GetBodyForLookupsModel(),
                "q"
            );
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();

            //act
            var expression = mapper.MapToOperator(selectorLambdaOperatorDescriptor).Build();
            var list = RequestHelpers.GetObjectSelect
            (
                new Business.Requests.GetTypedDropDownListRequest
                {
                    Selector = selectorLambdaOperatorDescriptor,
                    ModelType = typeof(LookUpsModel).AssemblyQualifiedName,
                    DataType = typeof(LookUps).AssemblyQualifiedName,
                    ModelReturnType = typeof(IEnumerable<LookUpsModel>).AssemblyQualifiedName,
                    DataReturnType = typeof(IEnumerable<LookUps>).AssemblyQualifiedName
                },
                repository,
                mapper
            ).Result.DropDownList.ToList();

            //assert
            AssertFilterStringIsCorrect(expression, "q => Convert(q.Where(l => (l.ListName == \"Credits\")).OrderByDescending(l => l.NumericValue).Select(l => new LookUpsModel() {NumericValue = l.NumericValue, Text = l.Text}))");
            Assert.Equal(5, list.Count);
        }

        #region Helpers
        private SelectOperatorDescriptor GetDepartmentsBodyForDepartmentModelType()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                        MemberFullName = "Name"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                    SelectorParameterName = "d"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["DepartmentID"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "DepartmentID"
                        },
                        ["Name"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "Name"
                        }
                    },
                    NewType = typeof(DepartmentModel).AssemblyQualifiedName
                },
                SelectorParameterName = "d"
            };

        private SelectOperatorDescriptor GetDepartmentsBodyForLookupModelType()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                        MemberFullName = "Name"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Ascending,
                    SelectorParameterName = "d"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["NumericValue"] = new ConvertOperatorDescriptor
                        {
                            SourceOperand = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                                MemberFullName = "DepartmentID"
                            },
                            Type = typeof(double?).FullName
                        },
                        ["Text"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "d" },
                            MemberFullName = "Name"
                        }
                    },
                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                },
                SelectorParameterName = "d"
            };

        private SelectOperatorDescriptor GetBodyForAnonymousType()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new WhereOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                        FilterBody = new EqualsBinaryOperatorDescriptor
                        {
                            Left = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                MemberFullName = "ListName"
                            },
                            Right = new ConstantOperatorDescriptor
                            {
                                ConstantValue = "Credits",
                                Type = typeof(string).AssemblyQualifiedName
                            }
                        },
                        FilterParameterName = "l"
                    },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                        MemberFullName = "NumericValue"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorParameterName = "l"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["NumericValue"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "NumericValue"
                        },
                        ["Text"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Text"
                        }
                    },
                },
                SelectorParameterName = "l"
            };

        private SelectOperatorDescriptor GetBodyForLookupsModel()
            => new SelectOperatorDescriptor
            {
                SourceOperand = new OrderByOperatorDescriptor
                {
                    SourceOperand = new WhereOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "q" },
                        FilterBody = new EqualsBinaryOperatorDescriptor
                        {
                            Left = new MemberSelectorOperatorDescriptor
                            {
                                SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                                MemberFullName = "ListName"
                            },
                            Right = new ConstantOperatorDescriptor
                            {
                                ConstantValue = "Credits",
                                Type = typeof(string).AssemblyQualifiedName
                            }
                        },
                        FilterParameterName = "l"
                    },
                    SelectorBody = new MemberSelectorOperatorDescriptor
                    {
                        SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                        MemberFullName = "NumericValue"
                    },
                    SortDirection = LogicBuilder.Expressions.Utils.Strutures.ListSortDirection.Descending,
                    SelectorParameterName = "l"
                },
                SelectorBody = new MemberInitOperatorDescriptor
                {
                    MemberBindings = new Dictionary<string, OperatorDescriptorBase>
                    {
                        ["NumericValue"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "NumericValue"
                        },
                        ["Text"] = new MemberSelectorOperatorDescriptor
                        {
                            SourceOperand = new ParameterOperatorDescriptor { ParameterName = "l" },
                            MemberFullName = "Text"
                        }
                    },
                    NewType = typeof(LookUpsModel).AssemblyQualifiedName
                },
                SelectorParameterName = "l"
            };

        private SelectorLambdaOperatorDescriptor GetExpressionDescriptor<T, TResult>(OperatorDescriptorBase selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorDescriptor
            {
                Selector = selectorBody,
                SourceElementType = typeof(T).AssemblyQualifiedName,
                ParameterName = parameterName,
                BodyType = typeof(TResult).AssemblyQualifiedName
            };

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

                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                });
            }
            MapperConfiguration.AssertConfigurationIsValid();
            serviceProvider = new ServiceCollection()
                .AddDbContext<SchoolContext>
                (
                    options => options.UseSqlServer
                    (
                        @"Server=(localdb)\mssqllocaldb;Database=RequestHelpersTest;ConnectRetryCount=0"
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

            LookUpsModel[] lookups = new LookUpsModel[]
            {
                new  LookUpsModel { ListName = "Grades", Text="A", Value="A", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="B", Value="B", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="C", Value="C", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="D", Value="D", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="E", Value="E", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Grades", Text="F", Value="F", EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="One", NumericValue=1, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Two", NumericValue=2, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Three", NumericValue=3, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Four", NumericValue=4, EntityState = LogicBuilder.Domain.EntityStateType.Added },
                new  LookUpsModel { ListName = "Credits", Text="Five", NumericValue=5, EntityState = LogicBuilder.Domain.EntityStateType.Added }
            };
            await repository.SaveGraphsAsync<LookUpsModel, LookUps>(lookups);
        }
        #endregion Seed DB
    }
}
