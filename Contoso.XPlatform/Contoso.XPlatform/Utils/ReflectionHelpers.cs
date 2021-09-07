using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    internal static class ReflectionHelpers
    {
        public static void SetPropertyToDefaultValue(this object item, string propertyName)
        {
            PropertyInfo pInfo = item.GetType().GetProperty
            (
                propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            );
            pInfo.SetValue(item, Activator.CreateInstance(pInfo.PropertyType));
        }

        public static void SetPropertyValue(this object item, string propertyName, object propertyValue) 
            => item.GetType().GetProperty
            (
                propertyName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
            )
            .SetValue(item, propertyValue);

        public static object GetPropertyValue(this object item, string propertyName)
        {
            try
            {
                return item.GetType().GetProperty
                (
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                )
                .GetValue(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ ex.GetType().Name + " : " + ex.Message}");
                throw;
            }
        }


        public static T GetPropertyValue<T>(this object item, string propertyName)
        {
            try
            {
                return item.GetType().GetProperty
                (
                    propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
                )
                .GetValue(item)
                .GetPropertyValue<T>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{ ex.GetType().Name + " : " + ex.Message}");
                throw;
            }
        }

        private static T GetPropertyValue<T>(this object valueObject)
        {
            if (valueObject == null)
                return default;

            Type valueObjectType = valueObject.GetType();
            if (typeof(T) == valueObjectType)
                return (T)valueObject;

            return (T)Convert.ChangeType(valueObject, typeof(T));
        }

        internal static bool TryParse(this string toParse, Type type, out object result)
        {
            if (type == null)
                throw new ArgumentException($"{nameof(type)}: DD23F248-BA7B-42BD-B825-8EAE7715DF35");

            if (type == typeof(string))
            {
                result = toParse;
                return true;
            }

            if (typeof(Enum).IsAssignableFrom(type))
            {
                if (!int.TryParse(toParse, out int _) && !Enum.IsDefined(type, toParse))
                {
                    result = null;
                    return false;
                }

                result = Enum.Parse(type, toParse);
                return true;
            }

            if (type.IsNullableType())
                type = Nullable.GetUnderlyingType(type);

            MethodInfo method = type.GetMethods().First(MatchTryParseMethod);

            bool MatchTryParseMethod(MethodInfo info)
            {
                if (info.Name != "TryParse")
                    return false;

                ParameterInfo[] pInfos = info.GetParameters();
                if (pInfos.Length != 2)
                    return false;

                return pInfos[0].ParameterType == typeof(string) 
                    && pInfos[1].IsOut 
                    && pInfos[1].ParameterType.GetElementType() == type;
            }

            object[] args = new object[] { toParse, null };
            bool success = (bool)method.Invoke(null, args);
            result = success ? args[1] : null;

            return success;
        }

        /// <summary>
        /// Using typeof(IQueryable<>).MakeGenericType(elementType).AssemblyQualifiedName returns the type
        /// name for the Xamarin platform e.g.
        /// "System.Linq.IQueryable`1[[{elementType.AssemblyQualifiedName}]], System.Core, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"
        /// which won't work on the server.
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        internal static string GetIQueryableTypeString(this Type elementType)
            => typeof(IQueryable<>).MakeGenericType(elementType).AssemblyQualifiedName;
        //wrong about this
        //=> $"System.Linq.IQueryable`1[[{elementType.AssemblyQualifiedName}]], System.Linq.Expressions, Version=4.2.2.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

        /// <summary>
        /// Using typeof(IEnumerable<>).MakeGenericType(elementType).AssemblyQualifiedName returns the type
        /// name for the Xamarin platform which does work e.g.
        /// "System.Collections.Generic.IEnumerable`1[[{elementType.AssemblyQualifiedName}]], mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"
        /// because the PublicKeyToken tokens are the same. This method added for completeness.
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        internal static string GetIEnumerableTypeString(this Type elementType)
            => typeof(IEnumerable<>).MakeGenericType(elementType).AssemblyQualifiedName;
            //wrong about this
            //=> $"System.Collections.Generic.IEnumerable`1[[{elementType.AssemblyQualifiedName}]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e";
    }
}
