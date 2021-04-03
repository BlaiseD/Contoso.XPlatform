using Contoso.Bsl.Configuration.ExpressionDescriptors;
using Contoso.Utils;
using System;

namespace Contoso.Bsl.Configuration.Json
{
    public class DescriptorConverter : JsonTypeConverter<OperatorDescriptorBase>
    {
        public override string TypePropertyName => "TypeString";
    }
}
