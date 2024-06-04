using System;

namespace YgoProDeck.Lib;

public class CardRequester {
    public Uri Uri { get; }

    public CardRequester(Uri uri) {
        Uri = uri;
    }

    //public async Task<CardResponse> GetCardAsync(CancellationToken cancellationToken) {
    //    using HttpClient client = new();
    //    using HttpResponseMessage response = await client.GetAsync(Uri, cancellationToken);

    //    if (!response.IsSuccessStatusCode) {
    //        throw new HttpRequestException($"Failed to get card data. Status code: {response.StatusCode}");
    //    }

    //    using Stream content = await response.Content.ReadAsStreamAsync(cancellationToken);
    //    return await JsonSerializer.DeserializeAsync<CardResponse>(content, cancellationToken: cancellationToken);
    //}
}
