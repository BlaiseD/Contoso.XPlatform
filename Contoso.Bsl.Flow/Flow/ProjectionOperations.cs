using AutoMapper;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.Bsl.Flow
{
    internal static class ProjectionOperations<TModel, TData> where TModel : BaseModel where TData : BaseData
    {
        public static TModel Get(IContextRepository repository,
            IMapper mapper,
            IExpressionParameter filterExpression,
            IExpressionParameter queryFunc = null,
            SelectExpandDefinitionParameters expansion = null)
            => GetItems
            (
                repository,
                mapper,
                filterExpression,
                queryFunc,
                expansion
            ).SingleOrDefault();

        public static ICollection<TModel> GetItems(IContextRepository repository,
            IMapper mapper,
            IExpressionParameter filterExpression = null,
            IExpressionParameter queryFunc = null,
            SelectExpandDefinitionParameters expansion = null) 
            => repository.GetAsync<TModel, TData>
            (
                GetFilter(mapper.MapToOperator(filterExpression)),
                GetQueryFunc(mapper.MapToOperator(queryFunc)),
                mapper.MapExpansion(expansion)
            ).Result;

        public static Expression<Func<TModel, bool>> GetFilter(IExpressionPart filterExpression)
            => (Expression<Func<TModel, bool>>)filterExpression?.Build();

        public static Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> GetQueryFunc(IExpressionPart selectorExpression)
            => (Expression<Func<IQueryable<TModel>, IQueryable<TModel>>>)selectorExpression?.Build();
    }
}
