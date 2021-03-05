using Contoso.Bsl.Configuration.ExpressionDescriptors;
using Contoso.Data;
using Contoso.Domain;
using LogicBuilder.EntityFrameworkCore.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Bsl.Utils
{
    public static class RequestHelpers
    {
        public static async Task<IEnumerable<dynamic>> GetSelect<TModel, TData>(this SelectorLambdaOperatorDescriptor request, IContextRepository contextRepository)
            where TModel : BaseModelClass
            where TData : BaseDataClass
        {
            return null;
        }
    }
}
