using AutoMapper;
using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Validators.Rules;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    internal class FieldsCollectionHelper
    {
        public FieldsCollectionHelper(EditFormSettingsDescriptor formSettings, ObservableCollection<IValidatable> properties, UiNotificationService uiNotificationService, IHttpService httpService)
        {
            this.formSettings = formSettings;
            this.properties = properties;
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
        }

        private readonly EditFormSettingsDescriptor formSettings;
        private readonly ObservableCollection<IValidatable> properties;
        private readonly UiNotificationService uiNotificationService;
        private readonly IHttpService httpService;

        public List<ValidateIf<TModel>> GetConditionalValidationConditions<TModel>(VariableDirectivesDictionary conditionalDirectives, IEnumerable<IValidatable> properties, IMapper mapper)
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

        public void CreateFieldsCollection()
        {
            formSettings.FieldSettings.ForEach
            (
                setting =>
                {
                    switch (setting.AbstractControlType)
                    {
                        case AbstractControlEnumDescriptor.FormControl:
                            AddFormControlSetting((FormControlSettingsDescriptor)setting);
                            break;
                    }
                }
            );
        }

        private void AddFormControlSetting(FormControlSettingsDescriptor setting)
        {
            if (setting.TextTemplate != null)
                AddTextControl(setting);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 0556AEAF-C851-44F1-A2A2-66C8814D0F54");
        }

        private void AddTextControl(FormControlSettingsDescriptor setting)
        {
            if (setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.TextTemplate)
                || setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.PasswordTemplate))
            {
                properties.Add(CreateEntryValidatableObject(setting));
            }
            else if (setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.DateTemplate))
            {
                properties.Add(CreateDatePickerValidatableObject(setting));
            }
        }

        private void AddDropdownControl(FormControlSettingsDescriptor setting)
        {
            properties.Add(CreatePickerValidatableObject(setting));
        }

        private IValidationRule[] GetValidationRules(FormControlSettingsDescriptor setting) 
            => setting.ValidationSetting.Validators.Select
            (
                validator => GetValidatorRule(validator, setting)
            ).ToArray();

        private IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting)
            => ValidatorRuleFactory.GetValidatorRule(validator, setting, formSettings.ValidationMessages, properties);

        private IValidatable CreateEntryValidatableObject(FormControlSettingsDescriptor setting) 
            => new EntryValidatableObject(setting, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = (string)ValidatableObjectFactory.GetValue(setting, string.Empty)
            };

        private IValidatable CreateDatePickerValidatableObject(FormControlSettingsDescriptor setting)
            => new DatePickerValidatableObject(setting, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = (DateTime)ValidatableObjectFactory.GetValue
                (
                    setting,
                    ValidatableObjectFactory.DefaultDateTime
                )
            };

        private IValidatable CreatePickerValidatableObject(FormControlSettingsDescriptor setting)
        {
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreatePickerValidatableObject",
                1,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(FormControlSettingsDescriptor)
                },
                null
            ).MakeGenericMethod(Type.GetType(setting.Type));

            return (IValidatable)methodInfo.Invoke(this, new object[] { setting });
        }

        private IValidatable _CreatePickerValidatableObject<T>(FormControlSettingsDescriptor setting) 
            => new PickerValidatableObject<T>(setting, this.httpService, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = default
            };
    }
}
