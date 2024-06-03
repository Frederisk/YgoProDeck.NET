using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

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
