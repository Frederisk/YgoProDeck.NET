using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

public enum FrameType {

    [Description("normal")]
    Normal,

    [Description("effect")]
    Effect,

    [Description("ritual")]
    Ritual,

    [Description("fusion")]
    Fusion,

    [Description("synchro")]
    Synchro,

    [Description("xyz")]
    XYZ,

    [Description("link")]
    Link,

    [Description("normal_pendulum")]
    NormalPendulum,

    [Description("effect_pendulum")]
    EffectPendulum,

    [Description("ritual_pendulum")]
    RitualPendulum,

    [Description("fusion_pendulum")]
    FusionPendulum,

    [Description("synchro_pendulum")]
    SynchroPendulum,

    [Description("xyz_pendulum")]
    XYZPendulum,

    [Description("spell")]
    Spell,

    [Description("trap")]
    Trap,

    [Description("token")]
    Token,

    [Description("skill")]
    Skill,
}
