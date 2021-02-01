﻿using AutoMapper;
using Contoso.Data;
using Contoso.Domain;
using Contoso.Parameters.Expressions;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.Bsl.Flow
{
    public static class QueryOperations<TModel, TData, TModelReturn, TDataReturn> where TModel : BaseModelClass, new() where TData : BaseDataClass
    {
        public static TModelReturn Get(IContextRepository repository,
            IMapper mapper,
            IExpressionParameter queryExpression)
        {
            return repository.QueryAsync<TModel, TData, TModelReturn, TDataReturn>
            (
                GetQueryFunc(mapper.MapToOperator(queryExpression))
            ).Result;
        }

        public static Expression<Func<IQueryable<TModel>, TModelReturn>> GetQueryFunc(IExpressionPart selectorExpression)
           => (Expression<Func<IQueryable<TModel>, TModelReturn>>)selectorExpression.Build();
    }
}
