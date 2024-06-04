using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using YgoProDeck.Lib.EnumValue;

namespace YgoProDeck.Lib.Response;

public record CardInfo {
    [JsonPropertyName("data")]
    public Datum[] Data { get; init; }
}

public record Datum {
    // All Cards
    public UInt64 ID {  get; init; }
    public String Name { get; init; }
    public CardType Type { get; init; }
    public FrameType FrameType { get; init; }
    public String Desc { get; init; }
    public Uri YgoProDeckUrl { get; init; }
    // Monster Card
    public UInt64? ATK { get; init; }
    public UInt64? DEF { get; init; }
    public UInt64? Level { get; init; }
    public MonsterAttribute? Attribute { get; init; }
    // Pendulum Monster
    public UInt64? Scale { get; init; }
    // Link Monster
    public UInt64? LinkValue { get; init; }
    public IReadOnlyList<LinkMarker>? LinkMarkers { get; init; }
    // Spell & Monster 
    public Race? Race { get; init; }
    public String? ArcheType { get; init; }

    // tcgplayer_data: set_edition set_url
}

public record CardImage {
    public UInt64 ID { get; init; }

    public Uri ImageUrl { get; init; }

    public Uri ImageUrlSmall { get; init; }

    public Uri ImageUrlCropped { get; init; }
}