using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Flow.Integration.Tests
{
    public class QueryOperationsTest
    {
        public QueryOperationsTest()
        {
            Initialize();
        }

        #region Fields
        private IServiceProvider serviceProvider;
        private static readonly string parameterName = "$it";
        #endregion Fields

        #region Tests
        [Fact]
        public void BuildWhere_OrderBy_ThenBy_Skip_Take_Average()
        {
            //arrange
            var bodyParameter = new AverageOperatorParameter
            (
                new TakeOperatorParameter
                (
                    new SkipOperatorParameter
                    (
                        new ThenByOperatorParameter
                        (
                            new OrderByOperatorParameter
                            (
                                new WhereOperatorParameter
                                (//q.Where(s => ((s.ID > 1) AndAlso (Compare(s.FirstName, s.LastName) > 0)))
                                    new ParameterOperatorParameter("q"),//q. the source operand
                                    new AndBinaryOperatorParameter//((s.ID > 1) AndAlso (Compare(s.FirstName, s.LastName) > 0)
                                    (
                                        new GreaterThanBinaryOperatorParameter
                                        (
                                            new MemberSelectorOperatorParameter("Id", new ParameterOperatorParameter("s")),
                                            new ConstantOperatorParameter(1, typeof(int))
                                        ),
                                        new GreaterThanBinaryOperatorParameter
                                        (
                                            new MemberSelectorOperatorParameter("FirstName", new ParameterOperatorParameter("s")),
                                            new MemberSelectorOperatorParameter("LastName", new ParameterOperatorParameter("s"))
                                        )
                                    ),
                                    "s"//s => (created in Where operator.  The parameter type is based on the source operand underlying type in this case Student.)
                                ),
                                new MemberSelectorOperatorParameter("LastName", new ParameterOperatorParameter("v")),
                                ListSortDirection.Ascending,
                                "v"
                            ),
                            new MemberSelectorOperatorParameter("FirstName", new ParameterOperatorParameter("v")),
                            ListSortDirection.Descending,
                            "v"
                        ),
                        2
                    ),
                    3
                ),
                new MemberSelectorOperatorParameter("Id", new ParameterOperatorParameter("j")),
                "j"
            );

            //act
            DoTest<StudentModel, Student, double, double>
            (
                bodyParameter, 
                "q",
                returnValue => Assert.True(returnValue > 1),
                "q => q.Where(s => ((s.ID > 1) AndAlso (s.FirstName.Compare(s.LastName) > 0))).OrderBy(v => v.LastName).ThenByDescending(v => v.FirstName).Skip(2).Take(3).Average(j => j.ID)"
            );
        }

        [Fact]
        public void BuildGroupBy_OrderBy_ThenBy_Skip_Take_Average()
        {
            //arrange
            var bodyParameter = new SelectOperatorParameter
            (
                new OrderByOperatorParameter
                (
                    new GroupByOperatorParameter
                    (
                        new ParameterOperatorParameter("q"),
                        new ConstantOperatorParameter(1, typeof(int)),
                        "a"
                    ),
                    new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("b")),
                    ListSortDirection.Ascending,
                    "b"
                ),
                new MemberInitOperatorParameter
                (
                    new Dictionary<string, IExpressionParameter>
                    {
                        ["Sum_budget"] = new SumOperatorParameter
                        (
                            new WhereOperatorParameter
                            (
                                new ParameterOperatorParameter("q"),
                                new AndBinaryOperatorParameter
                                (
                                    new NotEqualsBinaryOperatorParameter
                                    (
                                        new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("d")),
                                        new CountOperatorParameter(new ParameterOperatorParameter("q"))
                                    ),
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("d")),
                                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("c"))
                                    )
                                ),
                                "d"
                            ),
                            new MemberSelectorOperatorParameter("Budget", new ParameterOperatorParameter("item")),
                            "item"
                        )
                    }
                ),
                "c"
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<dynamic>, IQueryable<object>>
            (
                bodyParameter,
                "q",
                returnValue => Assert.True(returnValue.First().Sum_budget == 350000),
                "q => Convert(q.GroupBy(a => 1).OrderBy(b => b.Key).Select(c => new AnonymousType() {Sum_budget = q.Where(d => ((d.DepartmentID != q.Count()) AndAlso (d.DepartmentID == c.Key))).Sum(item => item.Budget)}))"
            );
        }

        [Fact]
        public void BuildGroupBy_AsQueryable_OrderBy_Select_FirstOrDefault()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameter
            (
                new SelectOperatorParameter
                (
                    new OrderByOperatorParameter
                    (
                        new AsQueryableOperatorParameter
                        (
                            new GroupByOperatorParameter
                            (
                                new ParameterOperatorParameter("q"),
                                new ConstantOperatorParameter(1, typeof(int)),
                                "item"
                            )
                        ),
                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("group")),
                        ListSortDirection.Ascending,
                        "group"
                    ),
                    new MemberInitOperatorParameter
                    (
                        new Dictionary<string, IExpressionParameter>
                        {
                            ["Min_administratorName"] = new MinOperatorParameter
                            (
                                new WhereOperatorParameter
                                (
                                    new ParameterOperatorParameter("q"),
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new ConstantOperatorParameter(1, typeof(int)),
                                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("sel"))
                                    ),
                                    "d"
                                ),
                                new MemberSelectorOperatorParameter("AdministratorName", new ParameterOperatorParameter("item")),
                                "item"
                            ),
                            ["Count"] = new CountOperatorParameter
                            (
                                new WhereOperatorParameter
                                (
                                    new ParameterOperatorParameter("q"),
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new ConstantOperatorParameter(1, typeof(int)),
                                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("sel"))
                                    ),
                                    "d"
                                )
                            ),
                            ["Sum_budget"] = new SumOperatorParameter
                            (
                                new WhereOperatorParameter
                                (
                                    new ParameterOperatorParameter("q"),
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new ConstantOperatorParameter(1, typeof(int)),
                                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("sel"))
                                    ),
                                    "d"
                                ),
                                new MemberSelectorOperatorParameter("Budget", new ParameterOperatorParameter("item")),
                                "item"
                            ),
                            ["Min_budget"] = new MinOperatorParameter
                            (
                                new WhereOperatorParameter
                                (
                                    new ParameterOperatorParameter("q"),
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new ConstantOperatorParameter(1, typeof(int)),
                                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("sel"))
                                    ),
                                    "d"
                                ),
                                new MemberSelectorOperatorParameter("Budget", new ParameterOperatorParameter("item")),
                                "item"
                            ),
                            ["Min_startDate"] = new MinOperatorParameter
                            (
                                new WhereOperatorParameter
                                (
                                    new ParameterOperatorParameter("q"),
                                    new EqualsBinaryOperatorParameter
                                    (
                                        new ConstantOperatorParameter(1, typeof(int)),
                                        new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("sel"))
                                    ),
                                    "d"
                                ),
                                new MemberSelectorOperatorParameter("StartDate", new ParameterOperatorParameter("item")),
                                "item"
                            )
                        }
                    ),
                    "sel"
                )
            );

            //act
            DoTest<DepartmentModel, Department, dynamic, object>
            (
                bodyParameter,
                "q",
                returnValue => 
                {
                    Assert.True(returnValue.Min_administratorName == "Candace Kapoor");
                    Assert.True(returnValue.Count == 4);
                    Assert.True(returnValue.Sum_budget == 900000);
                    Assert.True(returnValue.Min_budget == 100000);
                    Assert.True(returnValue.Min_startDate == DateTime.Parse("2007-09-01"));
                },
                "q => Convert(q.GroupBy(item => 1).AsQueryable().OrderBy(group => group.Key).Select(sel => new AnonymousType() {Min_administratorName = q.Where(d => (1 == sel.Key)).Min(item => item.AdministratorName), Count = q.Where(d => (1 == sel.Key)).Count(), Sum_budget = q.Where(d => (1 == sel.Key)).Sum(item => item.Budget), Min_budget = q.Where(d => (1 == sel.Key)).Min(item => item.Budget), Min_startDate = q.Where(d => (1 == sel.Key)).Min(item => item.StartDate)}).FirstOrDefault())"
            );
        }

        [Fact]
        public void All_Filter()
        {
            //arrange
            var bodyParameter = new AllOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new OrBinaryOperatorParameter
                (
                    new EqualsBinaryOperatorParameter
                    (
                        new MemberSelectorOperatorParameter("AdministratorName", new ParameterOperatorParameter("a")),
                        new ConstantOperatorParameter("Kim Abercrombie")
                    ),
                    new EqualsBinaryOperatorParameter
                    (
                        new MemberSelectorOperatorParameter("AdministratorName", new ParameterOperatorParameter("a")),
                        new ConstantOperatorParameter("Fadi Fakhouri")
                    )
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, bool, bool>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.False(returnValue),
                "$it => $it.All(a => ((a.AdministratorName == \"Kim Abercrombie\") OrElse (a.AdministratorName == \"Fadi Fakhouri\")))"
            );
        }

        [Fact]
        public void Any_Filter()
        {
            //arrange
            var bodyParameter = new AnyOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("AdministratorName", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter("Kim Abercrombie")
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, bool, bool>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.True(returnValue),
                "$it => $it.Any(a => (a.AdministratorName == \"Kim Abercrombie\"))"
            );
        }

        [Fact]
        public void Any()
        {
            //arrange
            var bodyParameter = new AnyOperatorParameter
            (
                new ParameterOperatorParameter(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, bool, bool>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.True(returnValue),
                "$it => $it.Any()"
            );
        }

        [Fact]
        public void AsQueryable()
        {
            //arrange
            var bodyParameter = new AsQueryableOperatorParameter
            (
                new ParameterOperatorParameter(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<DepartmentModel>, IQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.True(returnValue.Count() == 4);
                },
                "$it => $it.AsQueryable()"
            );
        }

        [Fact]
        public void Average_Selector()
        {
            //arrange
            var bodyParameter = new AverageOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, double, double>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2.5, returnValue),
                "$it => $it.Average(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Average()
        {
            //arrange
            var bodyParameter = new AverageOperatorParameter
            (
                new SelectOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName),
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, double, double>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2.5, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Average()"
            );
        }

        [Fact]
        public void Count_Filter()
        {
            //arrange
            var bodyParameter = new CountOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue),
                "$it => $it.Count(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void Count()
        {
            //arrange
            var bodyParameter = new CountOperatorParameter
            (
                new ParameterOperatorParameter(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue),
                "$it => $it.Count()"
            );
        }

        [Fact]
        public void Distinct()
        {
            //arrange
            var bodyParameter = new ToListOperatorParameter
            (
                new DistinctOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName)
                )
            );

            //act
            DoTest<DepartmentModel, Department, List<DepartmentModel>, List<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue.Count()),
                "$it => $it.Distinct().ToList()"
            );
        }

        [Fact()]
        public void First_Filter_Throws_Exception()
        {
            //arrange
            var bodyParameter = new FirstOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(-1)
                ),
                "a"
            );

            //act
            Assert.Throws<AggregateException>
            (
                () => DoTest<DepartmentModel, Department, DepartmentModel, Department>
                (
                    bodyParameter,
                    parameterName,
                    returnValue => { },
                    "$it => $it.First(a => (a.DepartmentID == -1))"
                )
            );
        }

        [Fact]
        public void First_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new FirstOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.DepartmentID),
                "$it => $it.First(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void First()
        {
            //arrange
            var bodyParameter = new FirstOperatorParameter
            (
                new ParameterOperatorParameter(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.First()"
            );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_null()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(-1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Null(returnValue),
                "$it => $it.FirstOrDefault(a => (a.DepartmentID == -1))"
            );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.DepartmentID),
                "$it => $it.FirstOrDefault(a => (a.DepartmentID == 1))"
            );
        }

        [Fact]
        public void FirstOrDefault()
        {
            //arrange
            var bodyParameter = new FirstOrDefaultOperatorParameter
            (
                new ParameterOperatorParameter(parameterName)
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.FirstOrDefault()"
            );
        }

        [Fact(Skip = "Can't map/project IGrouping<,>")]
        public void GroupBy()
        {
            //arrange
            var bodyParameter = new GroupByOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                "a"
            );

            //act
            DoTest<CourseModel, Course, IQueryable<IGrouping<int, CourseModel>>, IQueryable<IGrouping<int, Course>>>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.True(returnValue.Count() > 2);
                },
                "$it => $it.GroupBy(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void GroupBy_Select()
        {
            //arrange
            var bodyParameter = new SelectOperatorParameter
            (
                new GroupByOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName),
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    "a"
                ),
                new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("b")),
                "b"
            );

            //act
            DoTest<CourseModel, Course, object, object>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.NotNull(returnValue);
                },
                "$it => Convert($it.GroupBy(a => a.DepartmentID).Select(b => b.Key))"
            );
        }

        [Fact]
        public void GroupBy_SelectCount()
        {
            //arrange
            var bodyParameter = new CountOperatorParameter
            (
                new SelectOperatorParameter
                (
                    new GroupByOperatorParameter
                    (
                        new ParameterOperatorParameter(parameterName),
                        new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                        "a"
                    ),
                    new MemberSelectorOperatorParameter("Key", new ParameterOperatorParameter("b")),
                    "b"
                )
            );

            //act
            DoTest<CourseModel, Course, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue =>
                {
                    Assert.True(returnValue > 2);
                },
                "$it => $it.GroupBy(a => a.DepartmentID).Select(b => b.Key).Count()"
            );
        }

        [Fact]
        public void Last_Filter_Throws_Exception()
        {
            //arrange
            var bodyParameter = new LastOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(-1)
                ),
                "a"
            );

            //act
            Assert.Throws<AggregateException>
            (
                () => DoTest<DepartmentModel, Department, DepartmentModel, Department>
                (
                    bodyParameter,
                    parameterName,
                    returnValue => { },
                    "$it => $it.Last(a => (a.DepartmentID == -1))"
                )
            );
        }

        [Fact]
        public void Last_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new LastOperatorParameter
            (
                new ToListOperatorParameter
                ( 
                    new ParameterOperatorParameter(parameterName)
                ),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(2)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2, returnValue.DepartmentID),
                "$it => $it.ToList().Last(a => (a.DepartmentID == 2))"
            );
        }

        [Fact]
        public void Last()
        {
            //arrange
            var bodyParameter = new LastOperatorParameter
            (
                new ToListOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName)
                )
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.ToList().Last()"
            );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_null()
        {
            //arrange
            var bodyParameter = new LastOrDefaultOperatorParameter
            (
                new ToListOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName)
                ),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(-1)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Null(returnValue),
                "$it => $it.ToList().LastOrDefault(a => (a.DepartmentID == -1))"
            );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_match()
        {
            //arrange
            var bodyParameter = new LastOrDefaultOperatorParameter
            (
                new ToListOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName)
                ),
                new EqualsBinaryOperatorParameter
                (
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    new ConstantOperatorParameter(2)
                ),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2, returnValue.DepartmentID),
                "$it => $it.ToList().LastOrDefault(a => (a.DepartmentID == 2))"
            );
        }

        [Fact]
        public void LastOrDefault()
        {
            //arrange
            var bodyParameter = new LastOrDefaultOperatorParameter
            (
                new ToListOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName)
                )
            );

            //act
            DoTest<DepartmentModel, Department, DepartmentModel, Department>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.NotNull(returnValue),
                "$it => $it.ToList().LastOrDefault()"
            );
        }

        [Fact]
        public void Max_Selector()
        {
            //arrange
            var bodyParameter = new MaxOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue),
                "$it => $it.Max(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Max()
        {
            var bodyParameter = new MaxOperatorParameter
            (
                new SelectOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName),
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Max()"
            );
        }

        [Fact]
        public void Min_Selector()
        {
            //arrange
            var bodyParameter = new MinOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue),
                "$it => $it.Min(a => a.DepartmentID)"
            );
        }

        [Fact]
        public void Min()
        {
            //arrange
            var bodyParameter = new MinOperatorParameter
            (
                new SelectOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName),
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    "a"
                )
            );

            //act
            DoTest<DepartmentModel, Department, int, int>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue),
                "$it => $it.Select(a => a.DepartmentID).Min()"
            );
        }

        [Fact]
        public void OrderBy()
        {
            //arrange
            var bodyParameter = new OrderByOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                ListSortDirection.Ascending,
                "a"
            );

            //act
            DoTest<DepartmentModel, Department, IQueryable<DepartmentModel>, IQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(1, returnValue.First().DepartmentID),
                ""//"$it => $it.OrderBy(a => a.DepartmentID).ToList()"
            );
        }

        [Fact]
        public void OrderByDescending()
        {
            //arrange
            var bodyParameter = new AsQueryableOperatorParameter(new OrderByOperatorParameter
            (
                new ParameterOperatorParameter(parameterName),
                new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                ListSortDirection.Descending,
                "a"
            ));

            //act
            DoTest<DepartmentModel, Department, IOrderedQueryable<DepartmentModel>, IOrderedQueryable<Department>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(4, returnValue.First().DepartmentID),
                "$it => $it.OrderByDescending(a => a.DepartmentID).AsQueryable()"
            );
        }

        [Fact]
        public void OrderByThenBy()
        {
            //arrange
            var bodyParameter = new ThenByOperatorParameter
            (
                new OrderByOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName),
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    ListSortDirection.Ascending,
                    "a"
                ),
                new MemberSelectorOperatorParameter("CourseID", new ParameterOperatorParameter("a")),
                ListSortDirection.Ascending,
                "a"
            );

            //act
            DoTest<CourseModel, Course, IOrderedQueryable<CourseModel>, IOrderedQueryable<Course>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2021, returnValue.First().CourseID),
                "$it => $it.OrderBy(a => a.DepartmentID).ThenBy(a => a.CourseID)"
            );
        }

        [Fact]
        public void OrderByThenByDescending()
        {
            //arrange
            var bodyParameter = new ThenByOperatorParameter
            (
                new OrderByOperatorParameter
                (
                    new ParameterOperatorParameter(parameterName),
                    new MemberSelectorOperatorParameter("DepartmentID", new ParameterOperatorParameter("a")),
                    ListSortDirection.Ascending,
                    "a"
                ),
                new MemberSelectorOperatorParameter("CourseID", new ParameterOperatorParameter("a")),
                ListSortDirection.Descending,
                "a"
            );

            //act
            DoTest<CourseModel, Course, IQueryable<CourseModel>, IQueryable<Course>>
            (
                bodyParameter,
                parameterName,
                returnValue => Assert.Equal(2042, returnValue.First().CourseID),
                "$it => $it.OrderBy(a => a.DepartmentID).ThenByDescending(a => a.CourseID)"
            );
        }

        void DoTest<TModel, TData, TModelReturn, TDataReturn>(IExpressionParameter bodyParameter, string parameterName, Action<TModelReturn> assert, string expectedExpressionString) where TModel : LogicBuilder.Domain.BaseModel where TData : LogicBuilder.Data.BaseData
        {
            //arrange
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();
            ISchoolRepository repository = serviceProvider.GetRequiredService<ISchoolRepository>();
            IExpressionParameter expressionParameter = GetExpressionParameter<IQueryable<TModel>, TModelReturn>(bodyParameter, parameterName);

            //act
            TModelReturn returnValue = QueryOperations<TModel, TData, TModelReturn, TDataReturn>.Get
            (
                repository,
                mapper,
                expressionParameter
            );

            Expression<Func<IQueryable<TModel>, TModelReturn>> expression = QueryOperations<TModel, TData, TModelReturn, TDataReturn>.GetQueryFunc
            (
                mapper.MapToOperator(expressionParameter)
            );

            //assert
            if (!string.IsNullOrEmpty(expectedExpressionString))
            {
                AssertFilterStringIsCorrect(expression, expectedExpressionString);
            }

            assert(returnValue);
        }
        #endregion Tests

        #region Helpers
        private IExpressionParameter GetExpressionParameter<T, TResult>(IExpressionParameter selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorParameter
            (
                selectorBody,
                typeof(T),
                parameterName,
                typeof(TResult)
            );

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
                        @"Server=(localdb)\mssqllocaldb;Database=SchoolContext2;ConnectRetryCount=0"
                    ),
                    ServiceLifetime.Singleton
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
