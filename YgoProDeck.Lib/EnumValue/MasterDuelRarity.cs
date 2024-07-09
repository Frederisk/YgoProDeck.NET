using System.ComponentModel;
using System.Text.Json.Serialization;

using YgoProDeck.Lib.Helper.Json;

namespace YgoProDeck.Lib.EnumValue;

[JsonConverter(typeof(EnumDescriptionJsonConverter<MasterDuelRarity>))]
public enum MasterDuelRarity {
    [Description("Common")]
    Common,

    [Description("Rare")]
    Rare,

    [Description("Super Rare")]
    SuperRare,

    [Description("Ultra Rare")]
    UltraRare,
}
