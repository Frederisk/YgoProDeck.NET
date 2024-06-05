using System;
using System.Text.Json.Serialization;

namespace YgoProDeck.Lib.Response;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public partial record ErrorInfo {
    [JsonPropertyName("error")]
    public String Error { get; init; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
