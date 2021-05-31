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
    public class FieldsCollectionHelper
    {
        public FieldsCollectionHelper(IFormGroupSettings formSettings, ObservableCollection<IValidatable> properties, UiNotificationService uiNotificationService, IHttpService httpService)
        {
            this.formSettings = formSettings;
            this.properties = properties;
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
        }

        private readonly IFormGroupSettings formSettings;
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
            => this.CreateFieldsCollection(this.formSettings.FieldSettings);

        private void CreateFieldsCollection(List<FormItemSettingsDescriptor> fieldSettings, string parentName = null)
        {
            fieldSettings.ForEach
            (
                setting =>
                {
                    switch (setting.AbstractControlType)
                    {
                        case AbstractControlEnumDescriptor.FormControl:
                            AddFormControl
                            (
                                (FormControlSettingsDescriptor)setting,
                                GetFieldName(setting.Field, parentName)
                            );
                            break;
                        case AbstractControlEnumDescriptor.MultiSelectFormControl:
                            AddMultiSelectControl
                            (
                                (MultiSelectFormControlSettingsDescriptor)setting,
                                GetFieldName(setting.Field, parentName)
                            );
                            break;
                        case AbstractControlEnumDescriptor.FormGroup:
                            AddFormGroupSettings((FormGroupSettingsDescriptor)setting, parentName);
                            break;
                        case AbstractControlEnumDescriptor.FormGroupArray:
                            AddFormGroupArray
                            (
                                (FormGroupArraySettingsDescriptor)setting,
                                GetFieldName(setting.Field, parentName)
                            );
                            break;
                    }
                }
            );
        }

        string GetFieldName(string field, string parentName)
                => parentName == null ? field : $"{parentName}.{field}";

        private void AddFormGroupSettings(FormGroupSettingsDescriptor setting, string parentName = null)
        {
            if (setting.FormGroupTemplate == null 
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 4817E6BF-0B48-4829-BAB8-7AD17E006EA7");

            switch(setting.FormGroupTemplate.TemplateName)
            {
                case FromGroupTemplateNames.InlineFormGroupTemplate:
                    AddFormGroupInline(setting, parentName);
                    break;
                case FromGroupTemplateNames.PopupFormGroupTemplate:
                    AddFormGroupPopup(setting, parentName);
                    break;
                default:
                    throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 6664DF64-DF69-415E-8AD2-2AEFC3FA4261");
            }
        }

        private void AddFormGroupPopup(FormGroupSettingsDescriptor setting, string parentName)
        {
            properties.Add(CreateFormValidatableObject(setting, GetFieldName(setting.Field, parentName)));
        }

        private void AddFormGroupInline(FormGroupSettingsDescriptor setting, string parentName) 
            => CreateFieldsCollection(setting.FieldSettings, GetFieldName(setting.Field, parentName));

        private void AddFormControl(FormControlSettingsDescriptor setting, string name)
        {
            if (setting.TextTemplate != null)
                AddTextControl(setting, name);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting, name);
            else
                throw new ArgumentException($"{nameof(setting)}: 0556AEAF-C851-44F1-A2A2-66C8814D0F54");
        }

        private void AddTextControl(FormControlSettingsDescriptor setting, string name)
        {
            if (setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.TextTemplate)
                || setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.PasswordTemplate))
            {
                properties.Add(CreateEntryValidatableObject(setting, name));
            }
            else if (setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.DateTemplate))
            {
                properties.Add(CreateDatePickerValidatableObject(setting, name));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.TextTemplate.TemplateName)}: BFCC0C85-244A-4896-BAB2-0D29AD0F86D8");
            }
        }

        private void AddDropdownControl(FormControlSettingsDescriptor setting, string name)
        {
            if (setting.DropDownTemplate.TemplateName == nameof(QuestionTemplateSelector.PickerTemplate))
            {
                properties.Add(CreatePickerValidatableObject(setting, name));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 8A0325D9-E9B0-487D-B569-7E92CDBD4F30");
            }
        }

        private void AddFormGroupArray(FormGroupArraySettingsDescriptor setting, string name)
        {
            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 0B1F7121-915F-48B9-96A3-B410A67E6853");

            if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
            {
                properties.Add(CreateFormArrayValidatableObject(setting, name));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 5E4E494A-E3FE-4016-ABB3-F238DC8E72F9");
            }
        }

        private void AddMultiSelectControl(MultiSelectFormControlSettingsDescriptor setting, string name)
        {
            if (setting.MultiSelectTemplate.TemplateName == nameof(QuestionTemplateSelector.MultiSelectTemplate))
            {
                properties.Add(CreateMultiSelectValidatableObject(setting, name));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 880DF2E6-97E8-49F2-B88C-FE8DB4F01C63");
            }
        }

        private IValidationRule[] GetValidationRules(FormControlSettingsDescriptor setting) 
            => setting.ValidationSetting?.Validators?.Select
            (
                validator => GetValidatorRule(validator, setting)
            ).ToArray();

        private IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting)
            => ValidatorRuleFactory.GetValidatorRule(validator, setting, this.formSettings.ValidationMessages, properties);

        private IValidatable CreateFormValidatableObject(FormGroupSettingsDescriptor setting, string name)
        {
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreateFormValidatableObject",
                1,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(FormGroupSettingsDescriptor),
                    typeof(string)
                },
                null
            ).MakeGenericMethod(Type.GetType(setting.ModelType));

            return (IValidatable)methodInfo.Invoke(this, new object[] { setting, name });
        }

        private IValidatable _CreateFormValidatableObject<T>(FormGroupSettingsDescriptor setting, string name) where T : class
            => new FormValidatableObject<T>(name, setting, new IValidationRule[] { }, this.uiNotificationService, this.httpService)
            {
                Value = default
            };

        private IValidatable CreateEntryValidatableObject(FormControlSettingsDescriptor setting, string name)
        {
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreateEntryValidatableObject",
                1,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(FormControlSettingsDescriptor),
                    typeof(string)
                },
                null
            ).MakeGenericMethod(Type.GetType(setting.Type));

            return (IValidatable)methodInfo.Invoke(this, new object[] { setting, name });
        }

        private IValidatable _CreateEntryValidatableObject<T>(FormControlSettingsDescriptor setting, string name) 
            => new EntryValidatableObject<T>(name, setting, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = (T)ValidatableObjectFactory.GetValue(setting, default(T))
            };

        private IValidatable CreateDatePickerValidatableObject(FormControlSettingsDescriptor setting, string name)
            => new DatePickerValidatableObject(name, setting, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = (DateTime)ValidatableObjectFactory.GetValue
                (
                    setting,
                    ValidatableObjectFactory.DefaultDateTime
                )
            };

        private IValidatable CreatePickerValidatableObject(FormControlSettingsDescriptor setting, string name)
        {
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreatePickerValidatableObject",
                1,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(FormControlSettingsDescriptor),
                    typeof(string)
                },
                null
            ).MakeGenericMethod(Type.GetType(setting.Type));

            return (IValidatable)methodInfo.Invoke(this, new object[] { setting, name });
        }

        private IValidatable _CreatePickerValidatableObject<T>(FormControlSettingsDescriptor setting, string name) 
            => new PickerValidatableObject<T>(name, setting, this.httpService, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = default
            };

        private IValidatable CreateMultiSelectValidatableObject(MultiSelectFormControlSettingsDescriptor setting, string name)
        {
            Type elemmentType = Type.GetType(setting.MultiSelectTemplate.ModelType);
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreateMultiSelectValidatableObject",
                2,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(MultiSelectFormControlSettingsDescriptor),
                    typeof(string)
                },
                null
            ).MakeGenericMethod(typeof(ObservableCollection<>).MakeGenericType(elemmentType), elemmentType);

            return (IValidatable)methodInfo.Invoke(this, new object[] { setting, name });
        }

        private IValidatable _CreateMultiSelectValidatableObject<T, E>(MultiSelectFormControlSettingsDescriptor setting, string name) where T : ObservableCollection<E>
            => new MultiSelectValidatableObject<T,E>(name, setting, this.httpService, GetValidationRules(setting), this.uiNotificationService)
            {
                Value = default
            };

        private IValidatable CreateFormArrayValidatableObject(FormGroupArraySettingsDescriptor setting, string name)
        {
            Type elemmentType = Type.GetType(setting.ModelType);
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreateFormArrayValidatableObject",
                2,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(FormGroupArraySettingsDescriptor),
                    typeof(string)
                },
                null
            ).MakeGenericMethod(typeof(ObservableCollection<>).MakeGenericType(elemmentType), elemmentType);

            return (IValidatable)methodInfo.Invoke(this, new object[] { setting, name });
        }

        private IValidatable _CreateFormArrayValidatableObject<T, E>(FormGroupArraySettingsDescriptor setting, string name) where T : ObservableCollection<E>
            => new FormArrayValidatableObject<T, E>(name, setting, new IValidationRule[] { }, this.uiNotificationService)
            {
                Value = default
            };
    }
}
