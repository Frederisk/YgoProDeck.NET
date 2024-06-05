using System;
using System.Threading;
using System.Threading.Tasks;

using YgoProDeck.Lib.Query;
using YgoProDeck.Lib.Response;

namespace YgoProDeck.Cli;

public class Program {
    public static async Task Main(String[] args) {
        QueryParameters parameters = new() { 
            //Number = 10,
            //FuzzyName = "\"C\"",
            //Misc = true,
            Number = 10,
        };
        CardRequester requester = new(parameters);
        
        CardInfo? cardInfo = await requester.RequestCardInfoAsync(CancellationToken.None);

        Console.WriteLine(cardInfo?.Data.Count);
    }
}

