using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

//[JsonConverter(typeof(EnumDescriptionJsonConverter<DateRegion>))]
public enum DateRegion {

    [Description("tcg")]
    TCG,

    [Description("ocg")]
    OCG,
}
