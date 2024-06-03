using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

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
