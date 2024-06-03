using System.ComponentModel;

namespace YgoProDeck.Lib.EnumValue;

public enum CardType {

    // Main Deck Types
    [Description("Effect Monster")]
    EffectMonster,

    [Description("Flip Effect Monster")]
    FlipEffectMonster,

    [Description("Flip Tuner Effect Monster")]
    FlipTunerEffectMonster,

    [Description("Gemini Monster")]
    GeminiMonster,

    [Description("Normal Monster")]
    NormalMonster,

    [Description("Normal Tuner Monster")]
    NormalTunerMonster,

    [Description("Pendulum Effect Monster")]
    PendulumEffectMonster,

    [Description("Pendulum Effect Ritual Monster")]
    PendulumEffectRitualMonster,

    [Description("Pendulum Flip Effect Monster")]
    PendulumFlipEffectMonster,

    [Description("Pendulum Normal Monster")]
    PendulumNormalMonster,

    [Description("Pendulum Tuner Effect Monster")]
    PendulumTunerEffectMonster,

    [Description("Ritual Effect Monster")]
    RitualEffectMonster,

    [Description("Ritual Monster")]
    RitualMonster,

    [Description("Spell Card")]
    SpellCard,

    [Description("Spirit Monster")]
    SpiritMonster,

    [Description("Toon Monster")]
    ToonMonster,

    [Description("Trap Card")]
    TrapCard,

    [Description("Tuner Monster")]
    TunerMonster,

    [Description("Union Effect Monster")]
    UnionEffectMonster,

    // Extra Deck Types
    [Description("Fusion Monster")]
    FusionMonster,

    [Description("Link Monster")]
    LinkMonster,

    [Description("Pendulum Effect Fusion Monster")]
    PendulumEffectFusionMonster,

    [Description("Synchro Monster")]
    SynchroMonster,

    [Description("Synchro Pendulum Effect Monster")]
    SynchroPendulumEffectMonster,

    [Description("Synchro Tuner Monster")]
    SynchroTunerMonster,

    [Description("XYZ Monster")]
    XYZMonster,

    [Description("XYZ Pendulum Effect Monster")]
    XYZPendulumEffectMonster,

    // Other Types
    [Description("Skill Card")]
    SkillCard,

    [Description("Token")]
    Token,
}
