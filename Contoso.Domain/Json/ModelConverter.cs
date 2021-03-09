using Contoso.Utils;

namespace Contoso.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<BaseModelClass>
    {
        public override string TypePropertyName => "TypeFullName";
    }
}
