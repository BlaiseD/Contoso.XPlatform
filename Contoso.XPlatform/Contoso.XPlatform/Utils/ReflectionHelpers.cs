using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Utils
{
    internal static class ReflectionHelpers
    {
        public static T GetPropertyValue<T>(this object item, Type itemType, string propertyName) 
            => (T)itemType.GetProperty(propertyName).GetValue(item);

        public static T GetPropertyValue<T>(this object item, string propertyName)
            => (T)item.GetType().GetProperty(propertyName).GetValue(item);
    }
}
