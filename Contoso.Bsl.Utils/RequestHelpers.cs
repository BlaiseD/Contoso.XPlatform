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
        public static async Task<GetDropDownListResponse> GetSelect(GetDropDownListRequest request, IContextRepository contextRepository, IMapper mapper)
        {
            IEnumerable<dynamic> list = await (Task<IEnumerable<dynamic>>)"GetSelect".GetSelectMethod
            (
                new Type[] 
                { 
                    typeof(SelectorLambdaOperatorDescriptor), 
                    typeof(IContextRepository), 
                    typeof(IMapper) 
                }
            ).MakeGenericMethod
            (
                typeof(BaseModelClass).Assembly.GetType(request.ModelType),
                typeof(BaseDataClass).Assembly.GetType(request.DataType)
            ).Invoke(null, new object[] { request.Selector, contextRepository, mapper });

            return new GetDropDownListResponse { DropDownList = list };
        }

        private static MethodInfo GetSelectMethod(this string methodName, Type[] types)
           => typeof(RequestHelpers).GetMethod(methodName, types);

        public static async Task<IEnumerable<dynamic>> GetSelect<TModel, TData>(this SelectorLambdaOperatorDescriptor request, IContextRepository contextRepository, IMapper mapper)
            where TModel : BaseModelClass
            where TData : BaseDataClass
        {
            return await Query<TModel, TData>
            (
                contextRepository,
                mapper.MapToOperator(request)
            );
        }

        static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();

        static Task<IEnumerable<dynamic>> Query<TModel, TData>(IContextRepository repository,
            IExpressionPart queryExpression)
            where TModel : BaseModel
            where TData : BaseData
            => repository.QueryAsync<TModel, TData, IEnumerable<dynamic>, IEnumerable<dynamic>>
            (
                (Expression<Func<IQueryable<TModel>, IEnumerable<dynamic>>>)queryExpression.Build(),
                (SelectExpandDefinition)null
            );
    }
}
