using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace YgoProDeck.Lib;

public class CardRequester {
    public String Uri { get; }

    public CardRequester(String uri) {
        Uri = uri;
    }

    public async Task<CardResponse> GetCardAsync() {
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(Uri);

        if (!response.IsSuccessStatusCode) {
            throw new HttpRequestException($"Failed to get card data. Status code: {response.StatusCode}");
        }

        String content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CardResponse>(content);
    }

}
