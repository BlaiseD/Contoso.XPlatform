using LogicBuilder.Attributes;
using System;

namespace Contoso.Bsl.Flow
{
    public static class TypeHelper
    {
        [AlsoKnownAs("Get Type")]
        public static Type GetType([ParameterEditorControl(ParameterControlType.TypeAutoComplete)]  string assemblyQualifiedTypeName) 
            => Type.GetType(assemblyQualifiedTypeName);
    }
}
