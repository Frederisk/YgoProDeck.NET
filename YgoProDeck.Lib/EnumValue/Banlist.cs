using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

//[JsonConverter(typeof(EnumDescriptionJsonConverter<Banlist>))]
public enum Banlist {

    [Description("TCG")]
    TCG,

    [Description("OCG")]
    OCG,

    [Description("Goat")]
    Goat,
}
