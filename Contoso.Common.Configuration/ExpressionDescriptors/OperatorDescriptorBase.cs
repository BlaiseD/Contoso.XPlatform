using Contoso.Bsl.Configuration.Json;
using System.Text.Json.Serialization;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    [JsonConverter(typeof(DescriptorConverter))]
    public abstract class OperatorDescriptorBase : IExpressionOperatorDescriptor
    {
        public string TypeString => this.GetType().AssemblyQualifiedName;
    }
}
