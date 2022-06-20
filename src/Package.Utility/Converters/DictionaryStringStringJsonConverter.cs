using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Package.Utility.Converters
{
    public class DictionaryStringStringJsonConverter: JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();
            var value = new Dictionary<string, string>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return value;

                string keyString = reader.GetString()!;
                reader.Read();
                string itemValue = reader.GetString()!;
                value.Add(keyString, itemValue);
            }
 
            throw new JsonException("Error Occured");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, string> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
 
            foreach (KeyValuePair<string, string> item in value)
            {
                writer.WriteString(item.Key.ToString(), item.Value);
            }
 
            writer.WriteEndObject();
        }
    }
}