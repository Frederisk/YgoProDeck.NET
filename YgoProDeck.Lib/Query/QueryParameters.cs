using System;
using System.Collections.Generic;

using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Helper.Query;

namespace YgoProDeck.Lib.Query;

public partial record QueryParameters {
    [QueryConverter("name", typeof(CardNameListQueryConverter))]
    public IReadOnlyList<String>? Name { get; init; }

    [QueryConverter("fname")]
    public String? FuzzyName { get; init; }

    [QueryConverter("id", typeof(EightNumberListQueryConverter))]
    public IReadOnlyList<UInt64>? ID { get; init; }

    [QueryConverter("konami_id", typeof(NumberListQueryConverter))]
    public IReadOnlyList<UInt64>? KonamiID { get; init; }

    [QueryConverter("type", typeof(EnumListDescriptionQueryConverter<CardType>))]
    public IReadOnlyList<CardType>? Type { get; init; }

    [QueryConverter("atk", typeof(ComparableNumberQueryConverter))]
    public (UInt64 Number, ValueCompare Compare)? ATK { get; init; }

    [QueryConverter("def", typeof(ComparableNumberQueryConverter))]
    public (UInt64 Number, ValueCompare Compare)? DEF { get; init; }

    [QueryConverter("level", typeof(ComparableNumberQueryConverter))]
    public (UInt64 Number, ValueCompare Compare)? Level { get; init; }

    [QueryConverter("race", typeof(EnumListDescriptionQueryConverter<Race>))]
    public IReadOnlyList<Race>? Race { get; init; }

    [QueryConverter("attribute", typeof(EnumListDescriptionQueryConverter<MonsterAttribute>))]
    public IReadOnlyList<MonsterAttribute>? Attribute { get; init; }

    [QueryConverter("link")]
    public UInt64? Link { get; init; }

    [QueryConverter("linkmarker", typeof(EnumListDescriptionQueryConverter<LinkMarker>))]
    public IReadOnlyList<LinkMarker>? LinkMarker { get; init; } // Flags

    [QueryConverter("scale")]
    public UInt64? Scale { get; init; }

    [QueryConverter("cardset")]
    public String? CardSet { get; init; }

    [QueryConverter("archetype")]
    public String? Archetype { get; init; }

    [QueryConverter("banlist", typeof(EnumDescriptionQueryConverter))]
    public Banlist? Banlist { get; init; }

    [QueryConverter("sort", typeof(EnumDescriptionQueryConverter))]
    public Sort? Sort { get; init; }

    [QueryConverter("format", typeof(EnumDescriptionQueryConverter))]
    public Format? Format { get; init; }

    [QueryConverter("misc", typeof(YesOrNullQueryConverter))]
    public Boolean Misc { get; init; } // Default: False

    [QueryConverter("staple", typeof(YesOrNullQueryConverter))]
    public Boolean Staple { get; init; } // Default: False

    [QueryConverter("has_effect")]
    public Boolean? HasEffect { get; init; }

    [QueryConverter("startdate", typeof(DateOnlyQueryConverter))]
    public DateOnly? StartDate { get; init; }

    [QueryConverter("enddate", typeof(DateOnlyQueryConverter))]
    public DateOnly? EndDate { get; init; }

    [QueryConverter("dateregion", typeof(EnumDescriptionQueryConverter))]
    public DateRegion? DateRegion { get; init; }

    [QueryConverter("language", typeof(LanguageQueryConverter))]
    public Language Language { get; init; } // Default: English

    [QueryConverter("tcgplayer_data")]
    public Boolean? TCGPlayerData { get; init; } // Note TCGPlayerData will always be responded if this property is not null

    [QueryConverter("num")]
    public UInt64? Number { get; init; }
}