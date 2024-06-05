using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.Helper.Json;

public class NumberBooleanJsonConverter : JsonConverter<Boolean> {

    public override Boolean Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return reader.GetInt32() == 1;
    }

    public override void Write(Utf8JsonWriter writer, Boolean value, JsonSerializerOptions options) {
        writer.WriteNumberValue(value ? 1 : 0);
    }
}
