using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Validators.Rules;
using Contoso.XPlatform.ViewModels.Validatables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal class FieldsCollectionHelper
    {
        private IFormGroupSettings formSettings;
        private ObservableCollection<IValidatable> properties;
        private readonly UiNotificationService uiNotificationService;
        private readonly IHttpService httpService;
        private readonly IMapper mapper;
        private readonly IFieldsCollectionBuilder fieldsCollectionBuilder;

        public FieldsCollectionHelper(IFormGroupSettings formSettings, UiNotificationService uiNotificationService, IHttpService httpService, IMapper mapper, IFieldsCollectionBuilder fieldsCollectionBuilder)
        {
            this.formSettings = formSettings;
            this.uiNotificationService = uiNotificationService;
            this.httpService = httpService;
            this.mapper = mapper;
            this.properties = new ObservableCollection<IValidatable>();
            this.fieldsCollectionBuilder = fieldsCollectionBuilder;
        }

        public ObservableCollection<IValidatable> CreateFields()
        {
            this.CreateFieldsCollection(this.formSettings.FieldSettings);
            return this.properties;
        }

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

            switch (setting.FormGroupTemplate.TemplateName)
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
            else if (setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.HiddenTemplate))
            {
                properties.Add(CreateHiddenValidatableObject(setting, name));
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
            return (IValidatable)Activator.CreateInstance
            (
                typeof(FormValidatableObject<>).MakeGenericType(Type.GetType(setting.ModelType)),
                name,
                setting,
                new IValidationRule[] { },
                this.uiNotificationService,
                this.mapper,
                this.fieldsCollectionBuilder
            );
        }

        private IValidatable CreateHiddenValidatableObject(FormControlSettingsDescriptor setting, string name)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(HiddenValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    name,
                    setting,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateEntryValidatableObject(FormControlSettingsDescriptor setting, string name)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(EntryValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    name,
                    setting,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateDatePickerValidatableObject(FormControlSettingsDescriptor setting, string name)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(DatePickerValidatableObject),
                    name,
                    setting,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreatePickerValidatableObject(FormControlSettingsDescriptor setting, string name)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(PickerValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    name,
                    setting,
                    this.httpService,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateMultiSelectValidatableObject(MultiSelectFormControlSettingsDescriptor setting, string name)
        {
            return GetValidatable(Type.GetType(setting.MultiSelectTemplate.ModelType));
            IValidatable GetValidatable(Type elementType)
                => ValidatableObjectFactory.GetValidatable
                (
                    Activator.CreateInstance
                    (
                        typeof(MultiSelectValidatableObject<,>).MakeGenericType
                        (
                            typeof(ObservableCollection<>).MakeGenericType(elementType),
                            elementType
                        ),
                        name,
                        setting,
                        this.httpService,
                        GetValidationRules(setting),
                        this.uiNotificationService
                    ),
                    setting
                );
        }

        private IValidatable CreateFormArrayValidatableObject(FormGroupArraySettingsDescriptor setting, string name)
        {
            return GetValidatable(Type.GetType(setting.ModelType));
            IValidatable GetValidatable(Type elementType)
                => (IValidatable)Activator.CreateInstance
                (
                    typeof(FormArrayValidatableObject<,>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType(elementType),
                        elementType
                    ),
                    name,
                    setting,
                    new IValidationRule[] { },
                    this.uiNotificationService,
                    this.mapper,
                    this.fieldsCollectionBuilder
                );
        }
    }
}
