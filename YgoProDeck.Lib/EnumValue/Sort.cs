using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.EnumValue;

//[JsonConverter(typeof(EnumDescriptionJsonConverter<Sort>))]
public enum Sort {

    [Description("atk")]
    ATK,

    [Description("def")]
    DEF,

    [Description("name")]
    Name,

    [Description("type")]
    Type,

    [Description("level")]
    Level,

    [Description("id")]
    ID,

    [Description("new")]
    New,
}
