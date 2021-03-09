﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Contoso.Utils
{
    public class ObjectConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert) 
            => typeToConvert == typeof(object);

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.None:
                case JsonTokenType.StartObject:
                case JsonTokenType.EndObject:
                case JsonTokenType.StartArray:
                case JsonTokenType.EndArray:
                case JsonTokenType.PropertyName:
                case JsonTokenType.Comment:
                    throw new JsonException();
                case JsonTokenType.Number:
                    if (reader.TryGetByte(out byte byteValue))
                        return byteValue;
                    else if (reader.TryGetInt16(out short shortValue))
                        return shortValue;
                    else if (reader.TryGetInt32(out int intValue))
                        return intValue;
                    else if (reader.TryGetInt64(out long longValue))
                        return longValue;
                    else if (reader.TryGetSingle(out float floatValue))
                        return floatValue;
                    else if (reader.TryGetDecimal(out decimal decimalValue))
                        return decimalValue;
                    else if (reader.TryGetDouble(out double doubleValue))
                        return doubleValue;

                    return 0;
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.Null:
                    return null;
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
