using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Bsl.Configuration.ExpressionDescriptors;
using Contoso.Bsl.Flow;
using Contoso.Bsl.Flow.Tests.Data;
using Contoso.Contexts;
using Contoso.Data.Entities;
using Contoso.Domain.Entities;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using Contoso.Repositories;
using Contoso.Stores;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.Strutures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Bsl.Flow.Tests
{
    public class QueryableExpressionTests
    {
        public QueryableExpressionTests()
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
            //act: body = q.Where(s => ((s.ID > 1) AndAlso (s.FirstName.Compare(s.LastName) > 0))).OrderBy(v => v.LastName).ThenByDescending(v => v.FirstName).Skip(2).Take(3).Average(j => j.ID)
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

            //lambdaExpression q => q.Where...
            var expressionParameter = GetExpressionParameter<IQueryable<Student>, double>(bodyParameter, "q");

            Expression<Func<IQueryable<Student>, double>> expression = GetExpression<IQueryable<Student>, double>(expressionParameter);

            //assert
            AssertFilterStringIsCorrect(expression, "q => q.Where(s => ((s.ID > 1) AndAlso (s.FirstName.Compare(s.LastName) > 0))).OrderBy(v => v.LastName).ThenByDescending(v => v.FirstName).Skip(2).Take(3).Average(j => j.ID)");
        }

        [Fact]
        public void BuildGroupBy_OrderBy_ThenBy_Skip_Take_Average()
        {
            //arrange
            Expression<Func<IQueryable<Department>, IQueryable<object>>> expression1 =
                q => q.GroupBy(a => 1)
                    .OrderBy(b => b.Key)
                    .Select
                    (
                        c => new
                        {
                            Sum_budget = q.Where
                            (
                                d => ((d.DepartmentID == q.Count())
                                    && (d.DepartmentID == c.Key))
                            )
                            .ToList()
                        }
                    );

            //act
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
                        ["Sum_budget"] = new ToListOperatorParameter
                        (
                            new WhereOperatorParameter
                            (
                                new ParameterOperatorParameter("q"),
                                new AndBinaryOperatorParameter
                                (
                                    new EqualsBinaryOperatorParameter
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
                            )
                        )
                    }
                ),
                "c"
            );

            //lambdaExpression q => q.Where...
            var expressionParameter = GetExpressionParameter<IQueryable<Department>, IQueryable<object>> (bodyParameter, "q");

            Expression<Func<IQueryable<Department>, IQueryable<object>>> expression = GetExpression<IQueryable<Department>, IQueryable<object>>(expressionParameter);

            //assert
            Assert.NotNull(expression);
        }

        [Fact]
        public void BuildGroupBy_AsQueryable_OrderBy_Select_FirstOrDefault()
        {
            //arrange
            Expression<Func<IQueryable<Department>, object>> expression1 =
                q => q.GroupBy(item => 1)
                .AsQueryable()
                .OrderBy(group => group.Key)
                .Select
                (
                    sel => new
                    {
                        Min_administratorName = q.Where(d => (1 == sel.Key)).Min(item => string.Concat(string.Concat(item.Administrator.LastName, " "), item.Administrator.FirstName)),
                        Count_name = q.Where(d => (1 == sel.Key)).Count(),
                        Sum_budget = q.Where(d => (1 == sel.Key)).Sum(item => item.Budget),
                        Min_budget = q.Where(d => (1 == sel.Key)).Min(item => item.Budget),
                        Min_startDate = q.Where(d => (1 == sel.Key)).Min(item => item.StartDate)
                    }
                )
                .FirstOrDefault();

            //act
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
                                new ConcatOperatorParameter
                                (
                                    new ConcatOperatorParameter
                                    (
                                        new MemberSelectorOperatorParameter("Administrator.LastName", new ParameterOperatorParameter("item")),
                                        new ConstantOperatorParameter(" ", typeof(string))
                                    ),
                                    new MemberSelectorOperatorParameter("Administrator.FirstName", new ParameterOperatorParameter("item"))
                                ),
                                "item"
                            ),
                            ["Count_name"] = new CountOperatorParameter
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

            //lambdaExpression q => q.Where...
            var expressionParameter = GetExpressionParameter<IQueryable<Department>, object>(bodyParameter, "q");

            Expression<Func<IQueryable<Department>, object>> expression = GetExpression<IQueryable<Department>, object>(expressionParameter);

            //assert
            Assert.NotNull(expression);
        }

        [Fact]
        public void All_Filter()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, bool>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.All(a => ((a.CategoryName == \"CategoryOne\") OrElse (a.CategoryName == \"CategoryTwo\")))");
            Assert.True(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AllOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new OrBinaryOperatorParameter
                            (
                                new EqualsBinaryOperatorParameter
                                (
                                    new MemberSelectorOperatorParameter("CategoryName", new ParameterOperatorParameter("a")),
                                    new ConstantOperatorParameter("CategoryOne")
                                ),
                                new EqualsBinaryOperatorParameter
                                (
                                    new MemberSelectorOperatorParameter("CategoryName", new ParameterOperatorParameter("a")),
                                    new ConstantOperatorParameter("CategoryTwo")
                                )
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Any_Filter()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, bool>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Any(a => (a.CategoryName == \"CategoryOne\"))");
            Assert.True(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AnyOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryName", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter("CategoryOne")
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Any()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, bool>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Any()");
            Assert.True(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AnyOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void AsQueryable()
        {
            //act
            var expression = CreateExpression<IEnumerable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, new List<Category> { new Category() });

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.AsQueryable()");
            Assert.True(result.GetType().IsIQueryable());

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AsQueryableOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Average_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, double>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Average(a => a.CategoryID)");
            Assert.Equal(1.5, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AverageOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Average()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, double>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Average()");
            Assert.Equal(1.5, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new AverageOperatorParameter
                        (
                            new SelectOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Count_Filter()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Count(a => (a.CategoryID == 1))");
            Assert.Equal(1, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new CountOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Count()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Count()");
            Assert.Equal(2, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new CountOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Distinct()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Distinct()");
            Assert.Equal(2, result.Count());

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new DistinctOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void First_Filter_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.First(a => (a.CategoryID == -1))");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void First_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.First(a => (a.CategoryID == 1))");
            Assert.Equal(1, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void First()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.First()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_null()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.FirstOrDefault(a => (a.CategoryID == -1))");
            Assert.Null(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOrDefaultOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void FirstOrDefault_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.FirstOrDefault(a => (a.CategoryID == 1))");
            Assert.Equal(1, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOrDefaultOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void FirstOrDefault()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.FirstOrDefault()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new FirstOrDefaultOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void GroupBy()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IQueryable<IGrouping<int, Product>>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.GroupBy(a => a.SupplierID)");
            Assert.Equal(1, result.Count());
            Assert.Equal(2, result.First().Count());
            Assert.Equal(3, result.First().First().SupplierID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new GroupByOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("SupplierID", new ParameterOperatorParameter("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Last_Filter_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Last(a => (a.CategoryID == -1))");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Last_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Last(a => (a.CategoryID == 2))");
            Assert.Equal(2, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(2)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Last()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Last()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_null()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.LastOrDefault(a => (a.CategoryID == -1))");
            Assert.Null(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOrDefaultOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void LastOrDefault_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.LastOrDefault(a => (a.CategoryID == 2))");
            Assert.Equal(2, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOrDefaultOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(2)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void LastOrDefault()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.LastOrDefault()");
            Assert.NotNull(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new LastOrDefaultOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Max_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Max(a => a.CategoryID)");
            Assert.Equal(2, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MaxOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Max()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Max()");
            Assert.Equal(2, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MaxOperatorParameter
                        (
                            new SelectOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Min_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Min(a => a.CategoryID)");
            Assert.Equal(1, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MinOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Min()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Min()");
            Assert.Equal(1, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new MinOperatorParameter
                        (
                            new SelectOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderBy()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IOrderedQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderBy(a => a.CategoryID)");
            Assert.Equal(1, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new OrderByOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                            ListSortDirection.Ascending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderByDescending()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IOrderedQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderByDescending(a => a.CategoryID)");
            Assert.Equal(2, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new OrderByOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                            ListSortDirection.Descending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderByThenBy()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IOrderedQueryable<Product>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderBy(a => a.SupplierID).ThenBy(a => a.ProductID)");
            Assert.Equal(1, result.First().ProductID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new ThenByOperatorParameter
                        (
                            new OrderByOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("SupplierID", new ParameterOperatorParameter("a")),
                                ListSortDirection.Ascending,
                                "a"
                            ),
                            new MemberSelectorOperatorParameter("ProductID", new ParameterOperatorParameter("a")),
                            ListSortDirection.Ascending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void OrderByThenByDescending()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IOrderedQueryable<Product>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.OrderBy(a => a.SupplierID).ThenByDescending(a => a.ProductID)");
            Assert.Equal(2, result.First().ProductID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>() 
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new ThenByOperatorParameter
                        (
                            new OrderByOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("SupplierID", new ParameterOperatorParameter("a")),
                                ListSortDirection.Ascending,
                                "a"
                            ),
                            new MemberSelectorOperatorParameter("ProductID", new ParameterOperatorParameter("a")),
                            ListSortDirection.Descending,
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Paging()
        {
            //act
            var expression = CreateExpression<IQueryable<Product>, IQueryable<Address>>();
            var result = RunExpression(expression, GetProducts());

            //assert
            AssertFilterStringIsCorrect
            (
                expression,
                "$it => $it.SelectMany(a => a.AlternateAddresses).OrderBy(a => a.State).ThenBy(a => a.AddressID).Skip(1).Take(2)"
            );
            Assert.Equal(2, result.Count());
            Assert.Equal(4, result.First().AddressID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new TakeOperatorParameter
                        (
                            new SkipOperatorParameter
                            (
                                new ThenByOperatorParameter
                                (
                                    new OrderByOperatorParameter
                                    (
                                        new SelectManyOperatorParameter
                                        (
                                            new ParameterOperatorParameter(parameterName),
                                            new MemberSelectorOperatorParameter("AlternateAddresses", new ParameterOperatorParameter("a")),
                                            "a"
                                        ),
                                        new MemberSelectorOperatorParameter("State", new ParameterOperatorParameter("a")),
                                        ListSortDirection.Ascending,
                                        "a"
                                    ),
                                    new MemberSelectorOperatorParameter("AddressID", new ParameterOperatorParameter("a")),
                                    ListSortDirection.Ascending,
                                    "a"
                                ),
                                1
                            ),
                            2
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Select_New()
        {
            var expression = CreateExpression<IQueryable<Category>, IQueryable<dynamic>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Equal(2, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SelectOperatorParameter
                        (
                            new OrderByOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                ListSortDirection.Descending,
                                "a"
                            ),
                            new MemberInitOperatorParameter
                            (
                                new Dictionary<string, IExpressionParameter>
                                {
                                    ["CategoryID"] = new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                    ["CategoryName"] = new MemberSelectorOperatorParameter("CategoryName", new ParameterOperatorParameter("a")),
                                    ["Products"] = new MemberSelectorOperatorParameter("Products", new ParameterOperatorParameter("a"))
                                }
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void SelectMany()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Product>>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.SelectMany(a => a.Products)");
            Assert.Equal(3, result.Count());

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SelectManyOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("Products", new ParameterOperatorParameter("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Single_Filter_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Single(a => (a.CategoryID == -1))");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SingleOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Single_Filter_Returns_match()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Single(a => (a.CategoryID == 1))");
            Assert.Equal(1, result.CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SingleOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Single_with_multiple_matches_Throws_Exception()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, Category>();

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Single()");
            Assert.Throws<InvalidOperationException>(() => RunExpression(expression, GetCategories()));

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SingleOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Sum_Selector()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Sum(a => a.CategoryID)");
            Assert.Equal(3, result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SumOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Sum()
        {
            //act
            var expression = CreateExpression<IQueryable<Category>, int>();
            var result = RunExpression(expression, GetCategories());

            //assert
            AssertFilterStringIsCorrect(expression, "$it => $it.Select(a => a.CategoryID).Sum()");
            Assert.Equal(3, result);
            
            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new SumOperatorParameter
                        (
                            new SelectOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                "a"
                            )
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void ToList()
        {
            var expression = CreateExpression<IQueryable<Category>, List<Category>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Equal(2, result.Count);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new ToListOperatorParameter
                        (
                           new ParameterOperatorParameter(parameterName)
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Where_with_matches()
        {
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Equal(2, result.First().CategoryID);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new WhereOperatorParameter
                        (
                            new OrderByOperatorParameter
                            (
                                new ParameterOperatorParameter(parameterName),
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                ListSortDirection.Descending,
                                "a"
                            ),
                            new NotEqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

        [Fact]
        public void Where_without_matches()
        {
            var expression = CreateExpression<IQueryable<Category>, IQueryable<Category>>();
            var result = RunExpression(expression, GetCategories());

            Assert.Empty(result);

            Expression<Func<T, TReturn>> CreateExpression<T, TReturn>()
                => GetExpression<T, TReturn>
                (
                    GetExpressionParameter<T, TReturn>
                    (
                        new WhereOperatorParameter
                        (
                            new ParameterOperatorParameter(parameterName),
                            new EqualsBinaryOperatorParameter
                            (
                                new MemberSelectorOperatorParameter("CategoryID", new ParameterOperatorParameter("a")),
                                new ConstantOperatorParameter(-1)
                            ),
                            "a"
                        ),
                        parameterName
                    )
                );
        }

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
        #endregion Tests

        #region Helpers
        /// <summary>
        /// Takes an object describing the body e.g. $it.Any() and returns an object describing the lambda expressiom e.g. $it => $it.Any()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selectorBody"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        private IExpressionParameter GetExpressionParameter<T, TResult>(IExpressionParameter selectorBody, string parameterName = "$it")
            => new SelectorLambdaOperatorParameter
            (
                selectorBody,
                typeof(T),
                parameterName,
                typeof(TResult)
            );

        /// <summary>
        /// Takes an object describing the lambda expressiom e.g. $it => $it.Any() and returns the lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="completeLambda"></param>
        /// <returns></returns>
        private Expression<Func<T, TResult>> GetExpression<T, TResult>(IExpressionParameter completeLambda)
        {
            IMapper mapper = serviceProvider.GetRequiredService<IMapper>();

            return (Expression<Func<T, TResult>>)mapper.Map<SelectorLambdaOperator>//map the complete lambda from decriptor object to operator object
            (
                mapper.Map<IExpressionOperatorDescriptor>(completeLambda),//map the complete lambda from parameter object to decriptor object
                opts => opts.Items["parameters"] = GetParameters()
            ).Build();//create the lambda expression from the operator object
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
                    options =>
                    {
                        options.UseInMemoryDatabase("ContosoUniVersity");
                        options.UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider());
                    },
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
            context.Database.EnsureCreated();

            Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>()).Wait();
            //Task.Run(async () => await Seed_Database(serviceProvider.GetRequiredService<ISchoolRepository>())).Wait();
        }

        private static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();

        private TResult RunExpression<T, TResult>(Expression<Func<T, TResult>> filter, T instance)
            => filter.Compile().Invoke(instance);

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
        #endregion Helpers

        #region Queryables
        private IQueryable<Category> GetCategories()
         => new Category[]
            {
                new Category
                {
                    CategoryID = 1,
                    CategoryName = "CategoryOne",
                    Products = new Product[]
                    {
                        new Product
                        {
                            ProductID = 1,
                            ProductName = "ProductOne",
                            AlternateAddresses = new Address[]
                            {
                                new Address { AddressID = 1, City = "CityOne" },
                                new Address { AddressID = 2, City = "CityTwo"  },
                            }
                        },
                        new Product
                        {
                            ProductID = 2,
                            ProductName = "ProductTwo",
                            AlternateAddresses = new Address[]
                            {
                                new Address { AddressID = 3, City = "CityThree" },
                                new Address { AddressID = 4, City = "CityFour"  },
                            }
                        }
                    }
                },
                new Category
                {
                    CategoryID = 2,
                    CategoryName = "CategoryTwo",
                    Products =  new Product[]
                    {
                        new Product
                        {
                            AlternateAddresses = new Address[0]
                        }
                    }
                }
            }.AsQueryable();

        private IQueryable<Product> GetProducts()
         => new Product[]
         {
             new Product
             {
                 ProductID = 1,
                 ProductName = "ProductOne",
                 SupplierID = 3,
                 AlternateAddresses = new Address[]
                 {
                     new Address { AddressID = 1, City = "CityOne", State = "OH" },
                     new Address { AddressID = 2, City = "CityTwo", State = "MI"   },
                 }
             },
             new Product
             {
                 ProductID = 2,
                 ProductName = "ProductTwo",
                 SupplierID = 3,
                 AlternateAddresses = new Address[]
                 {
                     new Address { AddressID = 3, City = "CityThree", State = "OH"  },
                     new Address { AddressID = 4, City = "CityFour", State = "MI"   },
                 }
             }
         }.AsQueryable();
        #endregion Queryables

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
