using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.Helper.Json;

internal class DateOnlyJsonConverter : JsonConverter<DateOnly> {
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        String? rawString = reader.GetString() ?? throw new InvalidOperationException();
        return DateOnly.ParseExact(rawString, "yyyy-MM-dd");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
