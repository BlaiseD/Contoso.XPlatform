using LogicBuilder.Attributes;
using System;

namespace Contoso.Utils
{
    public static class TypeHelpers
    {
        public static string ToTypeString(this Type type)
            => type.IsGenericType && !type.IsGenericTypeDefinition
                ? type.AssemblyQualifiedName
                : type.FullName;

        [AlsoKnownAs("Get Type")]
        public static Type GetType([ParameterEditorControl(ParameterControlType.TypeAutoComplete)] string assemblyQualifiedTypeName)
            => Type.GetType(assemblyQualifiedTypeName);
    }
}
