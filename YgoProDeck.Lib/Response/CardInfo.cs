using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Helper.Json;

namespace YgoProDeck.Lib.Response;

public partial record CardInfo {
    [JsonPropertyName("data")]
    public IReadOnlyList<CardData> Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("meta")]
    public Meta? Meta { get; init; }
}

public partial record CardData {
    // All Cards
    [JsonPropertyName("id")]
    public UInt64 Id { get; set; }

    [JsonPropertyName("name")]
    public String Name { get; set; }

    [JsonPropertyName("type")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<CardType>))]
    public CardType Type { get; set; }

    [JsonPropertyName("frameType")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<FrameType>))]
    public FrameType FrameType { get; set; }

    [JsonPropertyName("desc")]
    public String Desc { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("pend_desc")]
    public String? PendDesc { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("monster_desc")]
    public String? MonsterDesc { get; set; }

    [JsonPropertyName("ygoprodeck_url")]
    public Uri YgoProDeckUrl { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("atk")]
    public UInt64? Atk { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("def")]
    public UInt64? Def { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("level")]
    public UInt64? Level { get; set; }

    [JsonPropertyName("race")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<Race>))]
    public String Race { get; set; } // Too many exceptions

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("attribute")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<MonsterAttribute>))]
    public MonsterAttribute? Attribute { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("card_sets")]
    public IReadOnlyList<CardSet>? CardSets { get; set; }

    [JsonPropertyName("card_images")]
    public IReadOnlyList<CardImage> CardImages { get; set; }

    [JsonPropertyName("card_prices")]
    public IReadOnlyList<CardPrice> CardPrices { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("archetype")]
    public String? Archetype { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("linkval")]
    public UInt64? LinkValue { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("linkmarkers")]
    //[JsonConverter(typeof(EnumDescriptionJsonConverter<LinkMarker>))]
    public IReadOnlyList<LinkMarker>? LinkMarkers { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("scale")]
    public UInt64? Scale { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("misc_info")]
    public IReadOnlyList<MiscInfo>? MiscInfo { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("banlist_info")]
    public BanlistInfo? BanlistInfo { get; set; }
}

public partial record CardImage {
    [JsonPropertyName("id")]
    public UInt64 Id { get; set; }

    [JsonPropertyName("image_url")]
    public Uri ImageUrl { get; set; }

    [JsonPropertyName("image_url_small")]
    public Uri ImageUrlSmall { get; set; }

    [JsonPropertyName("image_url_cropped")]
    public Uri ImageUrlCropped { get; set; }
}

public partial record CardPrice {
    [JsonPropertyName("cardmarket_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double CardmarketPrice { get; set; }

    [JsonPropertyName("tcgplayer_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double TcgplayerPrice { get; set; }

    [JsonPropertyName("ebay_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double EbayPrice { get; set; }

    [JsonPropertyName("amazon_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double AmazonPrice { get; set; }

    [JsonPropertyName("coolstuffinc_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double CoolstuffincPrice { get; set; }
}

public partial record CardSet {
    [JsonPropertyName("set_name")]
    public String SetName { get; set; }

    [JsonPropertyName("set_code")]
    public String SetCode { get; set; }

    [JsonPropertyName("set_rarity")]
    public String SetRarity { get; set; } // TODO: Is this a enum?

    [JsonPropertyName("set_rarity_code")]
    public String SetRarityCode { get; set; } // TODO: Is this a enum?

    [JsonPropertyName("set_price")]
    [JsonConverter(typeof(DoubleStringJsonConverter))]
    public Double SetPrice { get; set; }

    // TCG Player

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("set_edition")]
    public String? SetEdition { get; set; } // TODO: Is this a enum?

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("set_url")]
    public Uri? SetUrl { get; set; }
}

public partial record MiscInfo {
    [JsonPropertyName("views")]
    public UInt64 Views { get; set; }

    [JsonPropertyName("viewsweek")]
    public UInt64 Viewsweek { get; set; }

    [JsonPropertyName("upvotes")]
    public UInt64 Upvotes { get; set; }

    [JsonPropertyName("downvotes")]
    public UInt64 Downvotes { get; set; }

    [JsonPropertyName("formats")]
    //[JsonConverter(typeof(CollectionItemJsonConverter<Format, EnumDescriptionJsonConverter<Format>>))]
    public IReadOnlyList<Format> Formats { get; set; }

    [JsonPropertyName("tcg_date")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly TcgDate { get; set; }

    [JsonPropertyName("ocg_date")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly OcgDate { get; set; }

    [JsonPropertyName("konami_id")]
    public UInt64 KonamiId { get; set; }

    [JsonPropertyName("has_effect")]
    [JsonConverter(typeof(NumberBooleanJsonConverter))]
    public Boolean HasEffect { get; set; } // Number to Boolean
}

public partial record BanlistInfo {
    [JsonPropertyName("ban_tcg")]
    public BanStatus BanTcg { get; set; }

    [JsonPropertyName("ban_ocg")]
    public BanStatus BanOcg { get; set; }

    [JsonPropertyName("ban_goat")]
    public BanStatus BanGoat { get; set; }
}

public partial record Meta {
    [JsonPropertyName("current_rows")]
    public UInt64 CurrentRows { get; set; }

    [JsonPropertyName("total_rows")]
    public UInt64 TotalRows { get; set; }

    [JsonPropertyName("rows_remaining")]
    public UInt64 RowsRemaining { get; set; }

    [JsonPropertyName("total_pages")]
    public UInt64 TotalPages { get; set; }

    [JsonPropertyName("pages_remaining")]
    public UInt64 PagesRemaining { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("next_page")]
    public Uri? NextPage { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("next_page_offset")]
    public UInt64? NextPageOffset { get; set; }
}
