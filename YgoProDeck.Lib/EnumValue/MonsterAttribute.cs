using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.EnumValue;

[JsonConverter(typeof(EnumDescriptionJsonConverter<MonsterAttribute>))]
public enum MonsterAttribute {

    [Description("Dark")]
    Dark,

    [Description("Earth")]
    Earth,

    [Description("Fire")]
    Fire,

    [Description("Light")]
    Light,

    [Description("Water")]
    Water,

    [Description("Wind")]
    Wind,

    [Description("Divine")]
    Divine,
}
