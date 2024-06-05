using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.Helper.Json;

internal class EnumDescriptionJsonConverter<T> : JsonConverter<T> where T : Enum {
    private readonly Dictionary<Type, Dictionary<String, T>> _enumDescriptionCache = [];

    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        String? rawString = reader.GetString() ?? throw new JsonException("Expected string value.");

        //foreach (T value in Enum.GetValues(typeToConvert)) {
        //    var description = AttributeHelper.GetEnumDescription(value);
        //    if (String.Compare(rawString, description, true, CultureInfo.InvariantCulture) is 0) {
        //        return value;
        //    }
        //}

        if (!_enumDescriptionCache.TryGetValue(typeToConvert, out var descriptionMap)) {
            // Not found, create it
            //descriptionMap = Enum.GetValues(typeToConvert).Cast<T>().ToDictionary(
            //    value => AttributeHelper.GetEnumDescription(value),
            //    value => value,
            //    StringComparer.InvariantCultureIgnoreCase
            //    );
            descriptionMap = new(StringComparer.InvariantCultureIgnoreCase);
            foreach (T value in Enum.GetValues(typeToConvert)) {
                String description = AttributeHelper.GetEnumDescription(value);
                descriptionMap[description] = value;
            }
            // Cache it
            _enumDescriptionCache[typeToConvert] = descriptionMap;
        }

        // Read value from map
        if (descriptionMap.TryGetValue(rawString, out var result)) {
            return result;
        }

        throw new JsonException($"Unable to parse '{rawString}' to enum {typeToConvert.Name} by it's description.");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) {
        //throw new NotImplementedException();
        var description = AttributeHelper.GetEnumDescription(value);
        writer.WriteStringValue(description);
    }
}
