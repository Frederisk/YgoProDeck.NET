using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

//[JsonConverter(typeof(EnumDescriptionJsonConverter<ValueCompare>))]
public enum ValueCompare {

    [Description("")]
    Equal,

    [Description("lt")]
    LessThan,

    [Description("lte")]
    LessThanOrEqual,

    [Description("gt")]
    GreaterThan,

    [Description("gte")]
    GreaterThanOrEqual,
}
