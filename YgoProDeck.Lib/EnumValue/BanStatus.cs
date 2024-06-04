using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.EnumValue;

[JsonConverter(typeof(EnumDescriptionJsonConverter<BanStatus>))]
public enum BanStatus {

    [Description("Banned")] // Forbidden
    Forbidden,

    [Description("Limited")]
    Limited,

    [Description("Semi-Limited")]
    SemiLimited,
}
