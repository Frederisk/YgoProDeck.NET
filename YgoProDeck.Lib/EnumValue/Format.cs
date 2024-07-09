using System.ComponentModel;
using System.Text.Json.Serialization;

using YgoProDeck.Lib.Helper.Json;

namespace YgoProDeck.Lib.EnumValue;

[JsonConverter(typeof(EnumDescriptionJsonConverter<Format>))]
public enum Format {

    [Description("TCG")]
    TCG,

    [Description("OCG")]
    OCG,

    [Description("GOAT")]
    Goat,

    [Description("OCG GOAT")]
    OCGGoat,

    [Description("Speed Duel")]
    SpeedDuel,

    [Description("Rush Duel")]
    RushDuel,

    [Description("Duel Links")]
    DuelLinks,

    [Description("Master Duel")]
    MasterDuel,

    // Undocumented

    [Description("Common Charity")]
    CommonCharity,

    [Description("Edison")]
    Edison,
}
