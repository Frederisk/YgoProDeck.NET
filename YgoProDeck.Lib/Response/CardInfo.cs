using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Helper.Json;

namespace YgoProDeck.Lib.Response;

public partial record CardInfo {
    [JsonPropertyName("data")]
    public IReadOnlyList<CardData> Data { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("meta")]
    public Meta? Meta { get; init; }
}

public partial record CardData {
    // All Cards
    [JsonPropertyName("id")]
    public UInt64 Id { get; init; }

    [JsonPropertyName("name")]
    public String Name { get; init; }

    [JsonPropertyName("type")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<CardType>))]
    public CardType Type { get; init; }

    [JsonPropertyName("frameType")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<FrameType>))]
    public FrameType FrameType { get; init; }

    [JsonPropertyName("desc")]
    public String Desc { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("pend_desc")]
    public String? PendDesc { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("monster_desc")]
    public String? MonsterDesc { get; init; }

    [JsonPropertyName("ygoprodeck_url")]
    public Uri YgoProDeckUrl { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("atk")]
    public UInt64? Atk { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("def")]
    public UInt64? Def { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("level")]
    public UInt64? Level { get; init; }

    [JsonPropertyName("race")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<Race>))]
    public String Race { get; init; } // Too many exceptions

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("attribute")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<MonsterAttribute>))]
    public MonsterAttribute? Attribute { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("card_sets")]
    public IReadOnlyList<CardSet>? CardSets { get; init; }

    [JsonPropertyName("card_images")]
    public IReadOnlyList<CardImage> CardImages { get; init; }

    [JsonPropertyName("card_prices")]
    public IReadOnlyList<CardPrice> CardPrices { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("archetype")]
    public String? Archetype { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("linkval")]
    public UInt64? LinkValue { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("linkmarkers")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<LinkMarker>))]
    public IReadOnlyList<LinkMarker>? LinkMarkers { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("scale")]
    public UInt64? Scale { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("misc_info")]
    public IReadOnlyList<MiscInfo>? MiscInfo { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("banlist_info")]
    public BanlistInfo? BanlistInfo { get; init; }
}

public partial record CardImage {
    [JsonPropertyName("id")]
    public UInt64 Id { get; init; }

    [JsonPropertyName("image_url")]
    public Uri ImageUrl { get; init; }

    [JsonPropertyName("image_url_small")]
    public Uri ImageUrlSmall { get; init; }

    [JsonPropertyName("image_url_cropped")]
    public Uri ImageUrlCropped { get; init; }
}

public partial record CardPrice {
    [JsonPropertyName("cardmarket_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double CardmarketPrice { get; init; }

    [JsonPropertyName("tcgplayer_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double TcgplayerPrice { get; init; }

    [JsonPropertyName("ebay_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double EbayPrice { get; init; }

    [JsonPropertyName("amazon_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double AmazonPrice { get; init; }

    [JsonPropertyName("coolstuffinc_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double CoolstuffincPrice { get; init; }
}

public partial record CardSet {
    [JsonPropertyName("set_name")]
    public String SetName { get; init; }

    [JsonPropertyName("set_code")]
    public String SetCode { get; init; }

    [JsonPropertyName("set_rarity")]
    public String SetRarity { get; init; } // TODO: Is this a enum?

    [JsonPropertyName("set_rarity_code")]
    public String SetRarityCode { get; init; } // TODO: Is this a enum?

    [JsonPropertyName("set_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double SetPrice { get; init; }

    // TCG Player

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("set_edition")]
    public String? SetEdition { get; init; } // TODO: Is this a enum?

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("set_url")]
    public Uri? SetUrl { get; init; }
}

public partial record MiscInfo {
    [JsonPropertyName("views")]
    public UInt64 Views { get; init; }

    [JsonPropertyName("viewsweek")]
    public UInt64 Viewsweek { get; init; }

    [JsonPropertyName("upvotes")]
    public UInt64 Upvotes { get; init; }

    [JsonPropertyName("downvotes")]
    public UInt64 Downvotes { get; init; }

    [JsonPropertyName("formats")]
    //[JsonConverter(typeof(CollectionItemJsonConverter<Format, EnumDescriptionJsonConverter<Format>>))]
    public IReadOnlyList<Format> Formats { get; init; }

    [JsonPropertyName("tcg_date")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly TcgDate { get; init; }

    [JsonPropertyName("ocg_date")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly OcgDate { get; init; }

    [JsonPropertyName("konami_id")]
    public UInt64 KonamiId { get; init; }

    [JsonPropertyName("has_effect")]
    [JsonConverter(typeof(NumberBooleanJsonConverter))]
    public Boolean HasEffect { get; init; } // Number to Boolean
}

public partial record BanlistInfo {
    [JsonPropertyName("ban_tcg")]
    public BanStatus BanTcg { get; init; }

    [JsonPropertyName("ban_ocg")]
    public BanStatus BanOcg { get; init; }

    [JsonPropertyName("ban_goat")]
    public BanStatus BanGoat { get; init; }
}

public partial record Meta {
    [JsonPropertyName("current_rows")]
    public UInt64 CurrentRows { get; init; }

    [JsonPropertyName("total_rows")]
    public UInt64 TotalRows { get; init; }

    [JsonPropertyName("rows_remaining")]
    public UInt64 RowsRemaining { get; init; }

    [JsonPropertyName("total_pages")]
    public UInt64 TotalPages { get; init; }

    [JsonPropertyName("pages_remaining")]
    public UInt64 PagesRemaining { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("next_page")]
    public Uri? NextPage { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("next_page_offset")]
    public UInt64? NextPageOffset { get; init; }
}
