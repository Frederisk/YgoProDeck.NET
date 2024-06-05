using System;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.Response;
public partial record ErrorInfo {
    [JsonPropertyName("error")]
    public String Error { get; set; }
}
