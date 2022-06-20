using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Package.Domain;
using Package.Domain.Factories;

namespace Package.Utility.Converters
{
    public class RootPackageJsonConverter: JsonConverter<RootPackage>
    {
        public override RootPackage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var packageBuilder = new RootPackageBuilder();
            var idPropName = options.PropertyNamingPolicy?.ConvertName(nameof(RootPackage.Id)) ??
                             nameof(PackageItem.Id);
            var namePropName = options.PropertyNamingPolicy?.ConvertName(nameof(RootPackage.Name)) ??
                               nameof(PackageItem.Name);
            var itemsPropName = options.PropertyNamingPolicy?.ConvertName(nameof(RootPackage.Items)) ??
                                nameof(PackageItem.Children);
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (jsonDocument.RootElement.TryGetProperty(idPropName, out var idValue))
                    throw new JsonException($"Not found property {idPropName}");
                if (jsonDocument.RootElement.TryGetProperty(namePropName, out var nameValue))
                    throw new JsonException($"Not found property {namePropName}");

                packageBuilder.Id = idValue.GetString();
                packageBuilder.Name = idValue.GetString();

                if (jsonDocument.RootElement.TryGetProperty(itemsPropName, out var itemsValue))
                {
                    if (itemsValue.ValueKind != JsonValueKind.Array)
                        throw new JsonException($"Unexpected type of property {itemsPropName}");
                    int itemsCount = itemsValue.GetArrayLength();
                    for (int i = 0; i < itemsCount; i++)
                    {
                        var item = itemsValue[i].Deserialize<PackageItem>(options);
                        if (item == null)
                            throw new JsonException($"Error occured deserialize item {i}");
                        packageBuilder.AddChild(item);
                    }
                }
            }

            return packageBuilder.Build();
        }

        public override void Write(Utf8JsonWriter writer, RootPackage value, JsonSerializerOptions options)
        {
            var idPropName = options.PropertyNamingPolicy?.ConvertName(nameof(RootPackage.Id)) ?? nameof(RootPackage.Id);
            var namePropName = options.PropertyNamingPolicy?.ConvertName(nameof(RootPackage.Name)) ?? nameof(RootPackage.Name);
            var itemsPropName = options.PropertyNamingPolicy?.ConvertName(nameof(RootPackage.Items)) ?? nameof(RootPackage.Items);
            
            writer.WriteStartObject();
            writer.WritePropertyName(idPropName);
            writer.WriteStringValue(value.Id);
            writer.WritePropertyName(namePropName);
            writer.WriteStringValue(value.Name);
            writer.WritePropertyName(itemsPropName);
            writer.WriteStartArray();
            foreach (var child in value.Items)
            {
                var rawChild = JsonSerializer.Serialize(child, options);
                writer.WriteRawValue(rawChild);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}