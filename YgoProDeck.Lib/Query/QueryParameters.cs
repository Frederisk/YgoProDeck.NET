using System;
using System.Collections.Generic;

using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Helper.Query;

namespace YgoProDeck.Lib.Query;

/// <summary>
/// The query parameters for the card info request.
/// </summary>
public partial record QueryParameters {
    /// <summary>
    /// The exact name of the card.
    /// </summary>
    [QueryConverter("name", typeof(CardNameListQueryConverter))]
    public IReadOnlyList<String>? Name { get; init; }

    /// <summary>
    /// The fuzzy name of the card.
    /// </summary>
    [QueryConverter("fname")]
    public String? FuzzyName { get; init; }

    /// <summary>
    /// The 8-digit passcode of the card. You cannot pass this alongside name.
    /// </summary>
    [QueryConverter("id", typeof(EightNumberListQueryConverter))]
    public IReadOnlyList<UInt64>? ID { get; init; }

    /// <summary>
    /// The Konami ID of the card. This is not the passcode.
    /// </summary>
    [QueryConverter("konami_id", typeof(NumberListQueryConverter))]
    public IReadOnlyList<Int64>? KonamiID { get; init; }

    /// <summary>
    /// The type of card you want to filter by.
    /// </summary>
    [QueryConverter("type", typeof(EnumListDescriptionQueryConverter<CardType>))]
    public IReadOnlyList<CardType>? Type { get; init; }

    /// <summary>
    /// Filter by atk value.
    /// </summary>
    [QueryConverter("atk", typeof(ComparableNumberQueryConverter))]
    public (UInt64 Number, ValueCompare Compare)? ATK { get; init; }

    /// <summary>
    /// Filter by def value
    /// </summary>
    [QueryConverter("def", typeof(ComparableNumberQueryConverter))]
    public (UInt64 Number, ValueCompare Compare)? DEF { get; init; }

    /// <summary>
    /// Filter by card level/rank.
    /// </summary>
    [QueryConverter("level", typeof(ComparableNumberQueryConverter))]
    public (UInt64 Number, ValueCompare Compare)? Level { get; init; }

    /// <summary>
    /// Filter by the card race which is officially called type (Spellcaster, Warrior, Insect, etc). This is also used for Spell/Trap cards.
    /// </summary>
    [QueryConverter("race", typeof(EnumListDescriptionQueryConverter<Race>))]
    public IReadOnlyList<Race>? Race { get; init; }

    /// <summary>
    /// Filter by the card attribute.
    /// </summary>
    [QueryConverter("attribute", typeof(EnumListDescriptionQueryConverter<MonsterAttribute>))]
    public IReadOnlyList<MonsterAttribute>? Attribute { get; init; }

    /// <summary>
    /// Filter the cards by Link value.
    /// </summary>
    [QueryConverter("link")]
    public UInt64? Link { get; init; }

    /// <summary>
    /// Filter the cards by Link Marker value (Top, Bottom, Left, Right, Bottom-Left, Bottom-Right, Top-Left, Top-Right).
    /// </summary>
    [QueryConverter("linkmarker", typeof(EnumListDescriptionQueryConverter<LinkMarker>))]
    public IReadOnlyList<LinkMarker>? LinkMarker { get; init; } // Flags

    /// <summary>
    /// Filter the cards by Pendulum Scale value.
    /// </summary>
    [QueryConverter("scale")]
    public UInt64? Scale { get; init; }

    /// <summary>
    /// Filter the cards by card set (Metal Raiders, Soul Fusion, etc).
    /// </summary>
    [QueryConverter("cardset")]
    public String? CardSet { get; init; }

    /// <summary>
    /// Filter the cards by archetype (Dark Magician, Prank-Kids, Blue-Eyes, etc).
    /// </summary>
    [QueryConverter("archetype")]
    public String? Archetype { get; init; }

    /// <summary>
    /// Filter the cards by banlist (TCG, OCG, Goat).
    /// </summary>
    [QueryConverter("banlist", typeof(EnumDescriptionQueryConverter))]
    public Banlist? Banlist { get; init; }

    /// <summary>
    /// Sort the order of the cards.
    /// </summary>
    [QueryConverter("sort", typeof(EnumDescriptionQueryConverter))]
    public Sort? Sort { get; init; }

    /// <summary>
    /// Sort the format of the cards. Note: Duel Links is not 100% accurate but is close. Using tcg results in all cards with a set TCG Release Date and excludes Speed Duel/Rush Duel cards.
    /// </summary>
    [QueryConverter("format", typeof(EnumDescriptionQueryConverter))]
    public Format? Format { get; init; }

    /// <summary>
    /// Pass yes to show additional response info.
    /// </summary>
    [QueryConverter("misc", typeof(YesOrNullQueryConverter))]
    public Boolean Misc { get; init; } // Default: False

    /// <summary>
    /// Check if card is a staple.
    /// </summary>
    [QueryConverter("staple", typeof(YesOrNullQueryConverter))]
    public Boolean Staple { get; init; } // Default: False

    /// <summary>
    /// Check if a card actually has an effect or not by passing a boolean.
    /// </summary>
    [QueryConverter("has_effect")]
    public Boolean? HasEffect { get; init; }

    /// <summary>
    /// Filter based on cards' release date range. This is the start of the range.
    /// </summary>
    [QueryConverter("startdate", typeof(DateOnlyQueryConverter))]
    public DateOnly? StartDate { get; init; }

    /// <summary>
    /// Filter based on cards' release date range. This is the end of the range.
    /// </summary>
    [QueryConverter("enddate", typeof(DateOnlyQueryConverter))]
    public DateOnly? EndDate { get; init; }

    /// <summary>
    /// Filter based on cards' release date range. This is the region of the date.
    /// </summary>
    [QueryConverter("dateregion", typeof(EnumDescriptionQueryConverter))]
    public DateRegion? DateRegion { get; init; }

    /// <summary>
    /// Specify the language of the card info. Default is English. Note: Card images are only stored in English.
    /// </summary>
    [QueryConverter("language", typeof(LanguageQueryConverter))]
    public Language Language { get; init; } // Default: English

    /// <summary>
    /// Replace internal Card Set data with TCG-player Card Set Data.
    /// </summary>
    [QueryConverter("tcgplayer_data")]
    public Boolean? TCGPlayerData { get; init; } // Note TCGPlayerData will always be responded if this property is not null

    /// <summary>
    /// The number of cards to return. Must used with <see cref="Offset"/>.
    /// </summary>
    [QueryConverter("num")]
    public UInt64? Number { get; init; }

    /// <summary>
    /// The number of cards to skip. Must used with <see cref="Number"/>.
    /// </summary>
    [QueryConverter("offset")]
    public UInt64? Offset { get; init; }
}
