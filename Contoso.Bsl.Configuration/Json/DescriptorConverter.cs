using Contoso.Bsl.Configuration.ExpressionDescriptors;
using Contoso.Utils;
using System;

namespace Contoso.Bsl.Configuration.Json
{
    public class DescriptorConverter : JsonTypeConverter<IExpressionOperatorDescriptor>
    {
        public override string TypePropertyName => "TypeString";

        protected override Type GetDerivedType(string typeName)
            => Type.GetType(typeName, false);
    }
}
