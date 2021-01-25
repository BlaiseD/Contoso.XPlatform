using AutoMapper;
using Contoso.Data;
using Contoso.Repositories;
using Contoso.Domain;
using LogicBuilder.Attributes;
using LogicBuilder.EntityFrameworkCore.SqlServer;
using LogicBuilder.Expressions.Utils;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Bsl.Flow.Flow
{
    public static class CrudOperations<TModel, TData> where TModel : BaseModelClass, new() where TData : BaseDataClass
    {
        public static TModel GetModel
        (
            [Comments("Filter.")]
            IExpressionDescriptor filterDescriptor,

            [Comments("Variable reference to the repository.")]
            ISchoolRepository repository,

            [Comments("Variable reference to AutoMapper's IMapper.")]
            IMapper mapper,

            [Comments("Expansions and child expansions")]
            SelectExpandDefinition includes = null,

            [ParameterEditorControl(ParameterControlType.TypeAutoComplete)]
            [NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
            [Comments("Fully qualified class name for the model type.")]
            string modelType = null
        )
        {
            TModel model = repository.GetAsync<TModel, TData>
            (
                GetFilter<TModel>(filterDescriptor, mapper, "f"), 
                null, 
                includes
            ).Result.SingleOrDefault();

            return model ?? new TModel();
        }

        public static void SaveModel(IExpressionDescriptor filterDescriptor, TModel model, ISchoolRepository repository, IMapper mapper)
        {
            TModel existing = Task.Run
            (
                async () => await repository.GetItemsAsync<TModel, TData>
                (
                    GetFilter<TModel>(filterDescriptor, mapper, "f")
                )
            ).Result.SingleOrDefault();

            model.EntityState = existing != null
                    ? LogicBuilder.Domain.EntityStateType.Modified
                    : LogicBuilder.Domain.EntityStateType.Added;

            repository.SaveGraphAsync<TModel, TData>(model).Wait();
        }

        private static Expression<Func<T, bool>> GetFilter<T>(IExpressionDescriptor filterBody, IMapper mapper, string parameterName)
        {
            return (Expression<Func<T, bool>>)mapper.Map<FilterLambdaOperator>
            (
                new FilterLambdaDescriptor
                (
                    filterBody,
                    typeof(T),
                    parameterName
                ),
                opts => opts.Items[ExpressionOperators.PARAMETERS_KEY] = GetParameters()
            ).Build();
        }

        private static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();
    }
}
