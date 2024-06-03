using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

public enum Format {

    [Description("tcg")]
    TCG,

    [Description("ocg")]
    OCG,

    [Description("ocg goat")]
    OCGGoat,

    [Description("speed duel")]
    SpeedDuel,

    [Description("rush duel")]
    RushDuel,

    [Description("duel links")]
    DuelLinks,
}
