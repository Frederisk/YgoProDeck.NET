using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

//[Flags]
public enum LinkMarker {

    [Description("Top")]
    Top,

    [Description("Bottom")]
    Bottom,

    [Description("Left")]
    Left,

    [Description("Right")]
    Right,

    [Description("Bottom-Left")]
    BottomLeft,

    [Description("Bottom-Right")]
    BottomRight,

    [Description("Top-Left")]
    TopLeft,

    [Description("Top-Right")]
    TopRight,
}
