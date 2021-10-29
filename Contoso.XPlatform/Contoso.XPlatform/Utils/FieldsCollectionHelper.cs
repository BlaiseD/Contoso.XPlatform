﻿using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Validators.Rules;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Utils
{
    internal class FieldsCollectionHelper
    {
        private IFormGroupSettings formSettings;
        protected ObservableCollection<IValidatable> properties;
        private readonly UiNotificationService uiNotificationService;
        protected readonly IContextProvider contextProvider;
        private readonly string parentName;

        public FieldsCollectionHelper(IFormGroupSettings formSettings, IContextProvider contextProvider, ObservableCollection<IValidatable> properties = null, string parentName = null)
        {
            this.formSettings = formSettings;
            this.contextProvider = contextProvider;
            this.uiNotificationService = contextProvider.UiNotificationService;
            this.properties = properties ?? new ObservableCollection<IValidatable>();
            this.parentName = parentName;
        }

        public ObservableCollection<IValidatable> CreateFields()
        {
            this.CreateFieldsCollection();
            return this.properties;
        }

        private void CreateFieldsCollection()
        {
            this.formSettings.FieldSettings.ForEach
            (
                setting =>
                {
                    switch (setting)
                    {
                        case MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettings:
                            AddMultiSelectControl(multiSelectFormControlSettings);
                            break;
                        case FormControlSettingsDescriptor formControlSettings:
                            AddFormControl(formControlSettings);
                            break;
                        case FormGroupSettingsDescriptor formGroupSettings:
                            AddFormGroupSettings(formGroupSettings);
                            break;
                        case FormGroupArraySettingsDescriptor formGroupArraySettings:
                            AddFormGroupArray(formGroupArraySettings);
                            break;
                        default:
                            throw new ArgumentException($"{nameof(setting)}: B024F65A-50DC-4D45-B8F0-9EC0BE0E2FE2");
                    }
                }
            );
        }

        protected string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";

        private void AddFormGroupSettings(FormGroupSettingsDescriptor setting)
        {
            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 4817E6BF-0B48-4829-BAB8-7AD17E006EA7");

            switch (setting.FormGroupTemplate.TemplateName)
            {
                case FromGroupTemplateNames.InlineFormGroupTemplate:
                    AddFormGroupInline(setting);
                    break;
                case FromGroupTemplateNames.PopupFormGroupTemplate:
                    AddFormGroupPopup(setting);
                    break;
                default:
                    throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 6664DF64-DF69-415E-8AD2-2AEFC3FA4261");
            }
        }

        private void AddFormGroupPopup(FormGroupSettingsDescriptor setting)
        {
            properties.Add(CreateFormValidatableObject(setting));
        }

        protected virtual void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            new FieldsCollectionHelper
            (
                setting,
                this.contextProvider,
                this.properties,
                GetFieldName(setting.Field)
            ).CreateFields();
        }

        protected virtual void AddFormControl(FormControlSettingsDescriptor setting)
        {
            if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate);
            else if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: 0556AEAF-C851-44F1-A2A2-66C8814D0F54");
        }

        protected void AddTextControl(FormControlSettingsDescriptor setting, TextFieldTemplateDescriptor textTemplate)
        {
            if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.TextTemplate)
                || textTemplate.TemplateName == nameof(QuestionTemplateSelector.PasswordTemplate))
            {
                properties.Add
                (
                    CreateEntryValidatableObject
                    (
                        setting,
                        textTemplate.TemplateName,
                        setting.Placeholder,
                        setting.StringFormat
                    )
                );
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.DateTemplate))
            {
                properties.Add(CreateDatePickerValidatableObject(setting, textTemplate.TemplateName));
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.HiddenTemplate))
            {
                properties.Add(CreateHiddenValidatableObject(setting, textTemplate.TemplateName));
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.CheckboxTemplate))
            {
                properties.Add(CreateCheckboxValidatableObject(setting, textTemplate.TemplateName, setting.Title));
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.SwitchTemplate))
            {
                properties.Add(CreateSwitchValidatableObject(setting, textTemplate.TemplateName, setting.Title));
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.LabelTemplate))
            {
                properties.Add
                (
                    CreateLabelValidatableObject
                    (
                        setting,
                        textTemplate.TemplateName,
                        setting.Title,
                        setting.Placeholder,
                        setting.StringFormat
                    )
                );
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.TextTemplate.TemplateName)}: BFCC0C85-244A-4896-BAB2-0D29AD0F86D8");
            }
        }

        protected void AddDropdownControl(FormControlSettingsDescriptor setting)
        {
            if (setting.DropDownTemplate.TemplateName == nameof(QuestionTemplateSelector.PickerTemplate))
            {
                properties.Add(CreatePickerValidatableObject(setting, setting.DropDownTemplate));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 8A0325D9-E9B0-487D-B569-7E92CDBD4F30");
            }
        }

        private void AddFormGroupArray(FormGroupArraySettingsDescriptor setting)
        {
            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 0B1F7121-915F-48B9-96A3-B410A67E6853");

            if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
            {
                properties.Add(CreateFormArrayValidatableObject(setting));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 5E4E494A-E3FE-4016-ABB3-F238DC8E72F9");
            }
        }

        private void AddMultiSelectControl(MultiSelectFormControlSettingsDescriptor setting)
        {
            if (setting.MultiSelectTemplate.TemplateName == nameof(QuestionTemplateSelector.MultiSelectTemplate))
            {
                properties.Add(CreateMultiSelectValidatableObject(setting));
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 880DF2E6-97E8-49F2-B88C-FE8DB4F01C63");
            }
        }

        private IValidatable CreateFormValidatableObject(FormGroupSettingsDescriptor setting)
        {
            return (IValidatable)Activator.CreateInstance
            (
                typeof(FormValidatableObject<>).MakeGenericType(Type.GetType(setting.ModelType)),
                GetFieldName(setting.Field),
                setting,
                new IValidationRule[] { },
                this.contextProvider
            );
        }

        private IValidatable CreateHiddenValidatableObject(FormControlSettingsDescriptor setting, string templateName)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(HiddenValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    GetFieldName(setting.Field),
                    templateName,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateCheckboxValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(CheckboxValidatableObject),
                    GetFieldName(setting.Field),
                    templateName,
                    title,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateSwitchValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(SwitchValidatableObject),
                    GetFieldName(setting.Field),
                    templateName,
                    title,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateEntryValidatableObject(FormControlSettingsDescriptor setting, string templateName, string placeholder, string stringFormat)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(EntryValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    GetFieldName(setting.Field),
                    templateName,
                    placeholder,
                    stringFormat,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateLabelValidatableObject(FormControlSettingsDescriptor setting, string templateName, string title, string placeholder, string stringFormat)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(LabelValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    GetFieldName(setting.Field),
                    templateName,
                    title,
                    placeholder,
                    stringFormat,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreateDatePickerValidatableObject(FormControlSettingsDescriptor setting, string templateName)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(DatePickerValidatableObject),
                    GetFieldName(setting.Field),
                    templateName,
                    GetValidationRules(setting),
                    this.uiNotificationService
                ),
                setting
            );

        private IValidatable CreatePickerValidatableObject(FormControlSettingsDescriptor setting, DropDownTemplateDescriptor dropDownTemplate)
            => ValidatableObjectFactory.GetValidatable
            (
                Activator.CreateInstance
                (
                    typeof(PickerValidatableObject<>).MakeGenericType(Type.GetType(setting.Type)),
                    GetFieldName(setting.Field),
                    dropDownTemplate,
                    GetValidationRules(setting),
                    this.contextProvider
                ),
                setting
            );

        private IValidatable CreateMultiSelectValidatableObject(MultiSelectFormControlSettingsDescriptor setting)
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
                        GetFieldName(setting.Field),
                        setting,
                        GetValidationRules(setting),
                        this.contextProvider
                    ),
                    setting
                );
        }

        private IValidatable CreateFormArrayValidatableObject(FormGroupArraySettingsDescriptor setting)
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
                    GetFieldName(setting.Field),
                    setting,
                    new IValidationRule[] { },
                    this.contextProvider
                );
        }

        private IValidationRule[] GetValidationRules(FormControlSettingsDescriptor setting)
            => setting.ValidationSetting?.Validators?.Select
            (
                validator => GetValidatorRule(validator, setting)
            ).ToArray();

        private IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting)
            => new ValidatorRuleFactory(this.parentName).GetValidatorRule(validator, setting, this.formSettings.ValidationMessages, properties);
    }
}
