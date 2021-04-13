using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Validators.Rules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    internal class FieldsCollectionHelper
    {
        public FieldsCollectionHelper(EditFormSettingsDescriptor formSettings, ObservableCollection<IValidatable> properties)
        {
            FormSettings = formSettings;
            Properties = properties;
        }

        public EditFormSettingsDescriptor FormSettings { get; set; }
        public ObservableCollection<IValidatable> Properties { get; }

        public void CreateFieldsCollection()
        {
            FormSettings.FieldSettings.ForEach
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
                Properties.Add
                (
                    CreateEntryValidatableObject
                    (
                        setting.Field,
                        setting.TextTemplate.TemplateName,
                        setting.Placeholder,
                        (string)ValidatableObjectFactory.GetValue(setting, string.Empty),
                        GetValidationRules(setting)
                    )
                );
            }
            else if (setting.TextTemplate.TemplateName == nameof(QuestionTemplateSelector.DateTemplate))
            {
                Properties.Add
                (
                    CreateDatePickerValidatableObject
                    (
                        setting.Field,
                        setting.TextTemplate.TemplateName,
                        (DateTime)ValidatableObjectFactory.GetValue
                        (
                            setting,
                            ValidatableObjectFactory.DefaultDateTime
                        ),
                        GetValidationRules(setting)
                    )
                );
            }
        }

        private void AddDropdownControl(FormControlSettingsDescriptor setting)
        {
            throw new NotImplementedException();
        }

        private IValidationRule[] GetValidationRules(FormControlSettingsDescriptor setting) 
            => setting.ValidationSetting.Validators.Select
            (
                validator => GetValidatorRule(validator, setting)
            ).ToArray();

        private IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting)
            => ValidatorRuleFactory.GetValidatorRule(validator, setting, FormSettings.ValidationMessages, Properties);

        private IValidatable CreateEntryValidatableObject(string name, string templateName, string placeholder, string @value, params IValidationRule[] validationRules) 
            => new EntryValidatableObject(name, templateName, placeholder, validationRules)
            {
                Value = value
            };

        private IValidatable CreateDatePickerValidatableObject(string name, string templateName, DateTime @value, params IValidationRule[] validationRules)
            => new DatePickerValidatableObject(name, templateName, validationRules)
            {
                Value = value
            };

        private IValidatable CreatePickerValidatableObject(string name, Type elementType, string templateName, string title, object @value, System.Collections.IList itemsSource, params IValidationRule[] validationRules)
        {
            MethodInfo methodInfo = typeof(FieldsCollectionHelper).GetMethod
            (
                "_CreatePickerValidatableObject",
                1,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new Type[]
                {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    elementType,
                    typeof(List<>).MakeGenericType(elementType),
                    typeof(IValidationRule[])
                },
                null
            ).MakeGenericMethod(elementType);

            return (IValidatable)methodInfo.Invoke(this, new object[] { name, templateName, title, @value, itemsSource, validationRules });
        }

        private PickerValidatableObject<T> _CreatePickerValidatableObject<T>(string name, string templateName, string title, T @value, List<T> itemsSource, params IValidationRule[] validationRules)
            => new PickerValidatableObject<T>(name, templateName, title, itemsSource, validationRules)
            {
                Value = value
            };
    }
}
