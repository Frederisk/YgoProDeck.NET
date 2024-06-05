using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

//[JsonConverter(typeof(EnumDescriptionJsonConverter<Language>))]
public enum Language {

    [Description("")]
    English,

    [Description("fr")]
    French,

    [Description("de")]
    German,

    [Description("it")]
    Italian,

    [Description("pt")]
    Portuguese,

    // Undocumented

    [Description("ko")]
    Korean,

    [Description("ja")]
    Japanese,
}
