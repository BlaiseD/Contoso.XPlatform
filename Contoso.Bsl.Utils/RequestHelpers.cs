using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Configuration.ExpressionDescriptors;
using Contoso.Data;
using Contoso.Domain;
using LogicBuilder.Data;
using LogicBuilder.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("Contoso.Bsl.Flow.Integration.Tests")]
namespace Contoso.Bsl.Utils
{
    public static class RequestHelpers
    {
        public static async Task<GetAnonymousDropDownListResponse> GetAnonymousSelect(GetAnonymousDropDownListRequest request, IContextRepository contextRepository, IMapper mapper) 
            => await (Task<GetAnonymousDropDownListResponse>)"GetAnonymousSelect".GetSelectMethod
            (
                new Type[]
                {
                    typeof(GetAnonymousDropDownListRequest),
                    typeof(IContextRepository),
                    typeof(IMapper)
                }
            ).MakeGenericMethod
            (
                Type.GetType(request.ModelType),
                Type.GetType(request.DataType)
            ).Invoke(null, new object[] { request, contextRepository, mapper });

        public static async Task<GetAnonymousDropDownListResponse> GetAnonymousSelect<TModel, TData>(GetAnonymousDropDownListRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModel
            where TData : BaseData
            => new GetAnonymousDropDownListResponse
            {
                DropDownList = await Query<TModel, TData>
                (
                    contextRepository,
                    mapper.MapToOperator(request.Selector)
                )
            };

        public static async Task<GetLookupDropDownListResponse> GetLookupSelect(GetTypedDropDownListRequest request, IContextRepository contextRepository, IMapper mapper) 
            => await (Task<GetLookupDropDownListResponse>)"GetLookupSelect".GetSelectMethod
            (
                new Type[]
                {
                    typeof(GetTypedDropDownListRequest),
                    typeof(IContextRepository),
                    typeof(IMapper)
                }
            ).MakeGenericMethod
            (
                Type.GetType(request.ModelType),
                Type.GetType(request.DataType),
                Type.GetType(request.ModelReturnType),
                Type.GetType(request.DataReturnType)
            ).Invoke(null, new object[] { request, contextRepository, mapper });

        public static async Task<GetLookupDropDownListResponse> GetLookupSelect<TModel, TData, TModelReturn, TDataReturn>(GetTypedDropDownListRequest request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModel
            where TData : BaseData
            => new GetLookupDropDownListResponse
            {
                DropDownList = (IEnumerable<Domain.Entities.LookUpsModel>)await Query<TModel, TData, TModelReturn, TDataReturn>
                (
                    contextRepository,
                    mapper.MapToOperator(request.Selector)
                )
            };

        private static Task<IEnumerable<dynamic>> Query<TModel, TData>(IContextRepository repository,
            IExpressionPart queryExpression)
            where TModel : BaseModel
            where TData : BaseData 
            => repository.QueryAsync<TModel, TData, IEnumerable<dynamic>, IEnumerable<dynamic>>
            (
                (Expression<Func<IQueryable<TModel>, IEnumerable<dynamic>>>)queryExpression.Build(),
                (SelectExpandDefinition)null
            );

        private static Task<TModelReturn> Query<TModel, TData, TModelReturn, TDataReturn>(IContextRepository repository,
            IExpressionPart queryExpression)
            where TModel : BaseModel
            where TData : BaseData
            => repository.QueryAsync<TModel, TData, TModelReturn, TDataReturn>
            (
                (Expression<Func<IQueryable<TModel>, TModelReturn>>)queryExpression.Build(),
                (SelectExpandDefinition)null
            );

        private static MethodInfo GetSelectMethod(this string methodName, Type[] types)
           => typeof(RequestHelpers).GetMethods().Single(m => m.Name == methodName && m.IsGenericMethod);
    }
}
