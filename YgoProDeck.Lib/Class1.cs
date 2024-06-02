using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace YgoProDeck.Lib;

public class CardRequester {
}

public class CardQuery {
    public static readonly String BaseUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";

    //public String
}

public record class Parameters {
    [Description("name")]
    public String? Name { get; init; }
    [Description("fname")]
    public String? FuzzyName { get; init; }
    [Description("id")]
    public UInt64? ID { get; init; }
    [Description("konami_id")]
    public UInt64? KonamiID { get; init; }
    [Description("type")]
    public Type? Type { get; init; }
    [Description("atk")]
    public UInt64? ATK { get; init; }
    public ValueCompare? AtkCompare { get; init; }
    [Description("def")]
    public UInt64? DEF { get; init; }
    public ValueCompare? DefCompare { get; init; }
    [Description("level")]
    public UInt64? Level { get; init; }
    public ValueCompare? LevelCompare { get; init; }
    [Description("race")]
    public List<Race>? Race { get; init; }
    [Description("attribute")]
    public List<Attribute>? Attribute { get; init; }
    [Description("link")]
    public UInt64? Link { get; init; }
    [Description("linkmarker")]
    public List<LinkMarker>? LinkMarker { get; init; } // Flags
    [Description("scale")]
    public UInt64? Scale { get; init; }
    [Description("cardset")]
    public String? CardSet { get; init; }
    [Description("archetype")]
    public String? ArcheType { get; init; }
    [Description("banlist")]
    public Banlist? Banlist { get; init; }
    [Description("sort")]
    public Sort? Sort { get; init; }
    [Description("format")]
    public Format? Format { get; init; }
    [Description("misc")]
    public Boolean Misc { get; init; } // Default: False
    [Description("staple")]
    public String? Staple { get; init; }
    [Description("has_effect")]
    public Boolean? HasEffect { get; init; }
    [Description("startdate")]
    public DateOnly? StartDate { get; init; }
    [Description("enddate")]
    public DateOnly? EndDate { get; init; }
    [Description("dateregion")]
    public DateRegion? DateRegion { get; init; }
    [Description("language")]
    public Language Language { get; init; } // Default: English
}

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

public enum Attribute {
    [Description("Dark")]
    Dark,
    [Description("Earth")]
    Earth,
    [Description("Fire")]
    Fire,
    [Description("Light")]
    Light,
    [Description("Water")]
    Water,
    [Description("Wind")]
    Wind,
    [Description("Divine")]
    Divine,
}

public enum DateRegion {
    [Description("tcg")]
    TCG,
    [Description("ocg")]
    OCG,
}

public enum Race {
    // Monster Cards
    [Description("Aqua")]
    Aqua,
    [Description("Beast")]
    Beast,
    [Description("Beast-Warrior")]
    BeastWarrior,
    [Description("Creator-God")]
    CreatorGod,
    [Description("Cyberse")]
    Cyberse,
    [Description("Dinosaur")]
    Dinosaur,
    [Description("Divine-Beast")]
    DivineBeast,
    [Description("Dragon")]
    Dragon,
    [Description("Fairy")]
    Fairy,
    [Description("Fiend")]
    Fiend,
    [Description("Fish")]
    Fish,
    [Description("Insect")]
    Insect,
    [Description("Machine")]
    Machine,
    [Description("Plant")]
    Plant,
    [Description("Psychic")]
    Psychic,
    [Description("Pyro")]
    Pyro,
    [Description("Reptile")]
    Reptile,
    [Description("Rock")]
    Rock,
    [Description("Sea Serpent")]
    SeaSerpent,
    [Description("Spellcaster")]
    Spellcaster,
    [Description("Thunder")]
    Thunder,
    [Description("Warrior")]
    Warrior,
    [Description("Winged Beast")]
    WingedBeast,
    [Description("Wyrm")]
    Wyrm,
    [Description("Zombie")]
    Zombie,
    // Spell Cards
    [Description("Field")]
    Field,
    [Description("Equip")]
    Equip,
    [Description("Quick-Play")]
    QuickPlay,
    [Description("Ritual")]
    Ritual,
    // Trap Cards
    [Description("Counter")]
    Counter,
    // Spell and Trap Cards
    [Description("Normal")]
    Normal,
    [Description("Continuous")]
    Continuous,
}

public enum Type
{
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
}

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

public enum Banlist {
    [Description("TCG")]
    TCG,
    [Description("OCG")]
    OCG,
    [Description("Goat")]
    Goat,
}

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