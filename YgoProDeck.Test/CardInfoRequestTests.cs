using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using YgoProDeck.Lib.Query;
using YgoProDeck.Lib.Response;

namespace YgoProDeck.Test;

public class CardInfoRequestTests {

    [SetUp]
    public void Setup() { }

    public static IEnumerable<QueryParameters> GetQueryParameters() {
        return [
            new QueryParameters() { },
            new QueryParameters() { TCGPlayerData = true },
            new QueryParameters() { Misc = true },
        ];
    }

    [Test]
    [TestCaseSource(nameof(GetQueryParameters))]
    public async Task TestRequest(QueryParameters parameters) {
        CardInfoRequester requester = new(parameters);
        CardInfo? cardInfo = await requester.RequestAsync(CancellationToken.None);
        Assert.That(cardInfo.Data, Is.Not.Empty);
    }
}
