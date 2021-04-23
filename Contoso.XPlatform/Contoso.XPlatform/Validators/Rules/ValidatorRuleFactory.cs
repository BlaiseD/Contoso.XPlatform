using Contoso.Forms.Configuration.EditForm;
using Contoso.Forms.Configuration.Validation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Validators.Rules
{
    internal static class ValidatorRuleFactory
    {
        public static IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, ValidationMessageDictionary validationMessages, ObservableCollection<IValidatable> fields) 
            => (IValidationRule)typeof(ValidatorRuleFactory).GetMethod
            (
                "_GetValidatorRule",
                1,
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new Type[]
                {
                    typeof(ValidatorDefinitionDescriptor),
                    typeof(FormControlSettingsDescriptor),
                    typeof(ValidationMessageDictionary),
                    typeof(ObservableCollection<IValidatable>)
                },
                null
            )
            .MakeGenericMethod(Type.GetType(setting.Type)).Invoke
            (
                null,
                new object[]
                {
                    validator,
                    setting,
                    validationMessages,
                    fields
                }
            );

        private static IValidationRule _GetValidatorRule<T>(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, ValidationMessageDictionary validationMessages, ObservableCollection<IValidatable> fields)
        {
            if (validationMessages == null)
                throw new ArgumentException($"{nameof(validationMessages)}: C1BDA4F7-B684-438F-B5BB-B61F01B625CE");

            if (!validationMessages.TryGetValue(setting.Field, out ValidationMethodDictionary methodDictionary))
                throw new ArgumentException($"{nameof(setting.Field)}: 4FF12AAC-DF7F-4346-8747-52413FCA808F");

            if (!methodDictionary.TryGetValue(validator.ClassName, out string validationMessage))
                throw new ArgumentException($"{nameof(validator.ClassName)}: 8A45F637-347D-4578-9F9C-72E9026FBCEB");

            if (validator.ClassName == nameof(RequiredRule<T>))
                return GetRequiredRule();
            else if (validator.ClassName == nameof(IsMatchRule<T>))
                return GetIsMatchRule();
            else
                throw new ArgumentException($"{nameof(validator.ClassName)}: CF4FDB4D-F135-40E0-BB31-14DBA624FC25");

            IValidationRule GetRequiredRule()
                => new RequiredRule<T>
                (
                    setting.Field, 
                    validationMessage, 
                    fields, 
                    (T)setting.ValidationSetting.DefaultValue
                );

            IValidationRule GetIsMatchRule()
            {
                const string argumentName = "otherFieldName";
                if (!validator.Arguments.TryGetValue(argumentName, out ValidatorArgumentDescriptor validatorArgumentDescriptor))
                    throw new ArgumentException($"{argumentName}: ADB88D64-F9DA-4FC0-B9C0-CB910F86B735");

                return new IsMatchRule<T>
                (
                    setting.Field,
                    validationMessage, 
                    fields, 
                    (string)validatorArgumentDescriptor.Value
                );
            }
        }
    }
}
