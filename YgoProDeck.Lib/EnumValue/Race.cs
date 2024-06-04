using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.EnumValue;

//[JsonConverter(typeof(EnumDescriptionJsonConverter<Race>))]
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
