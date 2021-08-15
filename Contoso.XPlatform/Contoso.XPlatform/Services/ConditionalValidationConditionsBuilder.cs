﻿using AutoMapper;
using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Services
{
    public class ConditionalValidationConditionsBuilder : IConditionalValidationConditionsBuilder
    {
        private readonly IMapper mapper;

        public ConditionalValidationConditionsBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public List<ValidateIf<TModel>> GetConditions<TModel>(VariableDirectivesDictionary conditionalDirectives, IEnumerable<IValidatable> properties)
        {
            if (conditionalDirectives == null)
                return new List<ValidateIf<TModel>>();

            const string PARAMETERS_KEY = "parameters";
            List<ValidateIf<TModel>> list = new List<ValidateIf<TModel>>();

            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);

            foreach (var kvp in conditionalDirectives)
            {
                kvp.Value.ForEach
                (
                    descriptor =>
                    {
                        if (descriptor.Definition.ClassName == nameof(ValidateIf<TModel>))
                        {
                            var validatable = propertiesDictionary[kvp.Key];
                            validatable.Validations.ForEach
                            (
                                validationRule =>
                                {
                                    list.Add
                                    (
                                        new ValidateIf<TModel>
                                        {
                                            Field = kvp.Key,
                                            Validator = validationRule,
                                            Evaluator = (Expression<Func<TModel, bool>>)mapper.Map<FilterLambdaOperator>
                                            (
                                                descriptor.Condition,
                                                opts => opts.Items[PARAMETERS_KEY] = GetParameters()
                                            ).Build()
                                        }
                                    );
                                }
                            );
                        }
                    }
                );
            }

            return list;
        }

        private static IDictionary<string, ParameterExpression> GetParameters()
            => new Dictionary<string, ParameterExpression>();
    }
}