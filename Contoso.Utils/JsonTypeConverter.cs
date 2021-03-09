using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Contoso.Utils
{
    abstract public class JsonTypeConverter<T> : JsonConverter<T>
    {
        #region Properties
        abstract public string TypePropertyName { get; }
        #endregion Properties

        #region Methods
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert == typeof(T);

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!jsonDocument.RootElement.TryGetProperty(TypePropertyName, out var typeProperty))
                    throw new JsonException();

                return (T)JsonSerializer.Deserialize
                (
                    jsonDocument.RootElement.GetRawText(),
                    Type.GetType(typeProperty.GetString()),
                    options
                );
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) 
            => JsonSerializer.Serialize(writer, value, value.GetType(), options);
        #endregion Methods
    }
}
