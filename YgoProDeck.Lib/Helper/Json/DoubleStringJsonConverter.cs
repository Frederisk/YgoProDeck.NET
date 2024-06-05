using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.Helper.Json;

public class DoubleStringJsonConverter : JsonConverter<Double> {

    public override Double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        //return reader.GetDouble();
        return Double.Parse(reader.GetString() ?? throw new NullReferenceException());
    }

    public override void Write(Utf8JsonWriter writer, Double value, JsonSerializerOptions options) {
        writer.WriteNumberValue(value);
    }
}
