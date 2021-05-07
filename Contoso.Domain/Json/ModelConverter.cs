using Contoso.Utils;

namespace Contoso.Domain.Json
{
    public class ModelConverter : JsonTypeConverter<ViewModelBase>
    {
        public override string TypePropertyName => "TypeFullName";
    }
}
