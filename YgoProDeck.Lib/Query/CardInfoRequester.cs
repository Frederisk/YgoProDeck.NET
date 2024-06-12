using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using YgoProDeck.Lib.Helper.Query;
using YgoProDeck.Lib.Response;

namespace YgoProDeck.Lib.Query;

/// <summary>
/// The requester for card info.
/// </summary>
public class CardInfoRequester {
    public static readonly String BaseUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";

    public Uri Uri { get; }

    /// <summary>
    /// Create a new instance of <see cref="CardInfoRequester"/>.
    /// </summary>
    /// <param name="uri">
    /// The <see cref="Uri"/> to request card info.
    /// </param>
    public CardInfoRequester(Uri uri) {
        this.Uri = uri;
    }

    /// <summary>
    /// Create a new instance of <see cref="CardInfoRequester"/>.
    /// </summary>
    /// <param name="pars">
    /// The <see cref="QueryParameters"/> for the card info request.
    /// </param>
    public CardInfoRequester(QueryParameters pars) {
        this.Uri = CreateQueryURI(pars);
    }

    /// <summary>
    /// Request card info asynchronously.
    /// </summary>
    /// <param name="cancellationToken">
    /// The <see cref="CancellationToken"/> to cancel the operation.
    /// </param>
    /// <returns>
    /// The <see cref="CardInfo"/> if the request is successful; otherwise, <see langword="null"/>.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout.
    /// -or- failed to get card data.
    /// </exception>
    /// <exception cref="TaskCanceledException">
    /// The request failed due to timeout or user canceled the operation.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// The Uri is invalid.
    /// -or- Deserialization failed.
    /// </exception>
    /// <exception cref="JsonException">
    /// The JSON is invalid.
    /// -or- TValue is not compatible with the JSON.
    /// -or- There is remaining data in the stream.
    /// -or- Deserialization failed.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// There is no compatible <see cref="JsonConverter"/> for TValue
    /// or its serializable members.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Deserialization failed.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Deserialization failed.
    /// </exception>
    /// <exception cref="NullReferenceException">
    /// Deserialization failed.
    /// </exception>
    public async Task<CardInfo> RequestAsync(CancellationToken cancellationToken) {
        using HttpClient client = new();
        using HttpResponseMessage response = await client.GetAsync(this.Uri, cancellationToken);
        if (!response.IsSuccessStatusCode) {
            String? errorMessage = null;
            try {
                using Stream errorContent = await response.Content.ReadAsStreamAsync(cancellationToken);
                ErrorInfo? errorInfo = await JsonSerializer.DeserializeAsync<ErrorInfo>(errorContent, cancellationToken: cancellationToken);
                errorMessage = errorInfo?.Error;
            } catch (Exception) { /* ignore */ }

            throw new HttpRequestException($"Failed to get card data. Status code: {response.StatusCode}. Message: {errorMessage}");
        }

        using Stream content = await response.Content.ReadAsStreamAsync(cancellationToken);
        CardInfo? cardInfo = await JsonSerializer.DeserializeAsync<CardInfo>(content, cancellationToken: cancellationToken);
        return cardInfo!; // TODO: handle null
    }

    public async Task<CardInfo?> RequestAsync() => 
        await RequestAsync(CancellationToken.None);

    public static Uri CreateQueryURI(QueryParameters pars) {
        ArgumentNullException.ThrowIfNull(pars);

        UriBuilder uri = new(BaseUrl);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

        foreach (PropertyInfo property in pars.GetType().GetProperties()) {
            QueryConverterAttribute? attribute = property.GetCustomAttribute<QueryConverterAttribute>();
            if (attribute is null) {
                continue;
            }

            Object? propertyValue = property.GetValue(pars);
            if (propertyValue is null) {
                continue;
            }

            String? realValue = attribute.Converter.WriteValue(propertyValue);
            if (realValue is null) {
                continue;
            }

            query[attribute.Name] = realValue;
        }
        uri.Query = query.ToString();

        return uri.Uri;
    }
}
