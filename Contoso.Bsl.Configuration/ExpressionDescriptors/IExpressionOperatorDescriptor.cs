using Contoso.Bsl.Configuration.Json;
using Newtonsoft.Json;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    [JsonConverter(typeof(DescriptorConverter))]
    public interface IExpressionOperatorDescriptor
    {
    }
}
