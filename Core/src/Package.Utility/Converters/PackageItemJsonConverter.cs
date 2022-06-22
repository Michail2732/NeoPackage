using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Package.Domain;
using Package.Domain.Factories;

namespace Package.Utility.Converters
{
    public class PackageItemJsonConverter: JsonConverter<PackageItem>
    {
        public override PackageItem? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var itemBuilder = new PackageItemBuilder();
            using (var jsonDocument = JsonDocument.ParseValue(ref reader)) 
            {
                var idPropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Id)) ??
                                 nameof(PackageItem.Id);
                var namePropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Name)) ??
                                   nameof(PackageItem.Name);
                var propertiesPropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Properties)) ??
                                         nameof(PackageItem.Properties);
                var childPropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Children)) ??
                                    nameof(PackageItem.Children);

                if (jsonDocument.RootElement.TryGetProperty(idPropName, out var idValue))
                    throw new JsonException($"Not found property {idPropName}");
                if (jsonDocument.RootElement.TryGetProperty(namePropName, out var nameValue))
                    throw new JsonException($"Not found property {namePropName}");

                itemBuilder.Id = idValue.GetString();
                itemBuilder.Name = nameValue.GetString();

                if (jsonDocument.RootElement.TryGetProperty(propertiesPropName, out var propertiesValue))
                {
                    if (propertiesValue.ValueKind != JsonValueKind.Object)
                        throw new JsonException($"Unexpected type of property {propertiesPropName}");
                    var properties = propertiesValue.Deserialize<Dictionary<string, string>>(options);
                    if (properties != null)
                        foreach (var property in properties)
                            itemBuilder.Properties[property.Key] = property.Value;
                }

                if (jsonDocument.RootElement.TryGetProperty(childPropName, out var childrenValue))
                {
                    if (childrenValue.ValueKind != JsonValueKind.Array)
                        throw new JsonException($"Unexpected type of property {childPropName}");
                    int childrenCount = childrenValue.GetArrayLength();
                    for (int i = 0; i < childrenCount; i++)
                    {
                        var childItem = childrenValue[i].Deserialize<PackageItem>(options);
                        if (childItem == null)
                            throw new JsonException($"Error occured deserialize child {i}");
                        itemBuilder.AddChild(childItem);
                    }
                }
            }
            return itemBuilder.Build();
        }

        public override void Write(Utf8JsonWriter writer, PackageItem value, JsonSerializerOptions options)
        {
            var idPropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Id)) ?? nameof(PackageItem.Id);
            var namePropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Name)) ?? nameof(PackageItem.Name);
            var propertiesPropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Properties)) ?? nameof(PackageItem.Properties);
            var childPropName = options.PropertyNamingPolicy?.ConvertName(nameof(PackageItem.Children)) ?? nameof(PackageItem.Children);
            
            writer.WriteStartObject();
            writer.WritePropertyName(idPropName);
            writer.WriteStringValue(value.Id);
            writer.WritePropertyName(namePropName);
            writer.WriteStringValue(value.Name);
            writer.WritePropertyName(propertiesPropName);
            writer.WriteStartObject();
            writer.WriteRawValue(JsonSerializer.Serialize(value.Properties, options));
            writer.WriteEndObject();
            writer.WritePropertyName(childPropName);
            writer.WriteStartArray();
            foreach (var child in value.Children)
            {
                var rawChild = JsonSerializer.Serialize(child, options);
                writer.WriteRawValue(rawChild);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}