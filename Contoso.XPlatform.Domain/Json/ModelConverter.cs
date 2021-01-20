using Contoso.XPlatform.Utils;
using System;

namespace Contoso.XPlatform.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<BaseModelClass>
    {
        public override string TypePropertyName => "TypeFullName";
        protected override Type GetDerivedType(string typeName)
        {
            return typeof(BaseModelClass).Assembly.GetType(typeName, true, false);
        }
    }
}
