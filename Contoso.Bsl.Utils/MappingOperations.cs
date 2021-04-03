using AutoMapper;
using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using LogicBuilder.EntityFrameworkCore.SqlServer;
using LogicBuilder.Expressions.Utils.Expansions;
using LogicBuilder.Expressions.Utils.ExpressionBuilder;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("Contoso.Bsl.Flow.Integration.Tests")]
namespace Contoso.Bsl.Utils
{
    public static class MappingOperations
    {
        public static IExpressionPart MapToOperator(this IMapper mapper, IExpressionParameter expression)
            => mapper.MapToOperator
            (
                mapper.Map<OperatorDescriptorBase>(expression)
            );

        public static IExpressionPart MapToOperator(this IMapper mapper, OperatorDescriptorBase expression)
            => mapper.Map<IExpressionPart>
            (
                expression,
                opts => opts.Items[ExpressionOperators.PARAMETERS_KEY] = GetParameters()
            );

        public static SelectExpandDefinition MapExpansion(this IMapper mapper, SelectExpandDefinitionParameters expression)
            => mapper.MapExpansion
            (
                mapper.Map<SelectExpandDefinitionDescriptor>(expression)
            );

        public static SelectExpandDefinition MapExpansion(this IMapper mapper, SelectExpandDefinitionDescriptor expression)
            => mapper.Map<SelectExpandDefinition>
            (
                expression,
                opts => opts.Items[ExpressionOperators.PARAMETERS_KEY] = GetParameters()
            );

        public static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();
    }
}
