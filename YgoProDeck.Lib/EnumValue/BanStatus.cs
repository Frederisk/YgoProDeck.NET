using System.ComponentModel;
using System.Text.Json.Serialization;

using YgoProDeck.Lib.Helper.Json;

namespace YgoProDeck.Lib.EnumValue;

[JsonConverter(typeof(EnumDescriptionJsonConverter<BanStatus>))]
public enum BanStatus {

    [Description("Forbidden")]
    Forbidden,

    [Description("Limited")]
    Limited,

    [Description("Semi-Limited")]
    SemiLimited,
}
