using Contoso.Forms.Configuration.EditForm;
using System;
using System.Reflection;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    internal static class ValidatableObjectFactory
    {
        public static readonly DateTime DefaultDateTime = new DateTime(1900, 1, 1);

        public static object GetValue(FormControlSettingsDescriptor setting, object defaultValue) 
            => typeof(ValidatableObjectFactory)
                .GetMethod
                (
                    "_GetValue", 
                    1,
                    BindingFlags.NonPublic | BindingFlags.Static,
                    null,
                    new Type[] 
                    { 
                        typeof(FormControlSettingsDescriptor), 
                        typeof(object)
                    },
                    null
                )
                .MakeGenericMethod(Type.GetType(setting.Type))
                .Invoke(null, new object[] { setting, defaultValue });

        private static T _GetValue<T>(FormControlSettingsDescriptor setting, object defaultValue) 
            => (T)(setting?.ValidationSetting.DefaultValue ?? defaultValue);
    }
}
