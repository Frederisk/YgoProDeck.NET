using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types;
using Telegram.Bot;
using YgoProDeck.Lib.EnumValue;

using YgoProDeck.Lib.Helper;

using YgoProDeck.Lib.Query;

using YgoProDeck.Lib.Response;

namespace YgoProDeck.Telegram;

public static partial class Program {
    private static readonly SemaphoreSlim semaphore = new(1, 1);

    private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery) {
        if (String.IsNullOrWhiteSpace(inlineQuery.Query)) {
            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, []);
            return;
        }

        var (argsQuery, query) = SplitQuery(inlineQuery.Query);

        if (String.IsNullOrWhiteSpace(query)) {
            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, []);
            return;
        }

        QueryParameters par = new() {
            FuzzyName = query,
            Language = QueryGetLanguage(argsQuery),
            Offset = 0,
            Number = 15,
            Misc = true,
        };

        List<InlineQueryResult> results = [];

        if (await semaphore.WaitAsync(TimeSpan.FromMilliseconds(200))) {
            try {
                // Web
                CardInfoRequester requester = new(par);
                CardInfo a = (await requester.RequestAsync())!;

                foreach (CardData data in a.Data) {
                    String message = CreateCardInfoMessage(data);

                    results.Add(
                        new InlineQueryResultArticle(
                            data.Id.ToString("D8"),
                            data.Name,
                            new InputTextMessageContent(message) {
                                ParseMode = ParseMode.Html,
                            }
                        ) {
                            Description = AttributeHelper.GetEnumDescription(data.Type),
                        }
                    );
                }
            } catch (HttpRequestException) {
                // Web Error or Not Found
            } catch (Exception ex) {
                await Logging(botClient, "!!!Exception: " + ex.Message);
            } finally {
                semaphore.Release();
            }
        } else {
            await Logging(botClient, "Semaphore is locked");
        }

        await botClient.AnswerInlineQueryAsync(inlineQuery.Id, results);
    }

    private static (Dictionary<String, String> args, String? query) SplitQuery(String argsString) {
        IEnumerable<String> allQuery = argsString.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim());
        Dictionary<String, String> argsQuery = [];
        List<String> queries = [];
        foreach (String arg in allQuery) {
            var match = Regex.Match(arg, @"^\s*%\s*(\w*)\s*=\s*(.*?)\s*$", RegexOptions.Singleline);
            if (match.Success) {
                argsQuery.Add(match.Groups[1].Value, match.Groups[2].Value.Trim());
            } else {
                queries.Add(arg);
            }
        }
        return (argsQuery, queries.FirstOrDefault());
    }

    private static Language QueryGetLanguage(Dictionary<String, String> argsQuery) {
        String lang = argsQuery.TryGetValue("lang", out var value) ? value : "";
        var langEnum = lang switch {
            "en" => Language.English,
            "ja" => Language.Japanese,
            "ko" => Language.Korean,
            "fr" => Language.French,
            "de" => Language.German,
            "it" => Language.Italian,
            "pt" => Language.Portuguese,
            _ => Language.English,
        };
        return langEnum;
    }

    private static String CreateCardInfoMessage(CardData data) {
        StringBuilder builder = new();
        builder.AppendLine($"<b>{data.Name}</b>");
        builder.AppendLine();
        builder.AppendLine($"<b>CardType: </b>{AttributeHelper.GetEnumDescription(data.Type)}");
        builder.AppendLine($"<b>ID/KonamiID: </b>{data.Id}/{data.MiscInfo?.FirstOrDefault()?.KonamiId}");
        if (data.Attribute is not null) builder.AppendLine($"<b>Attribute: </b>{data.Attribute}");
        if (data.Race is not null) builder.AppendLine($"<b>Race: </b>{data.Race}");
        if (data.Level is not null) builder.AppendLine($"<b>Level/Rank: </b>{data.Level}");
        if (data.LinkValue is not null) builder.AppendLine($"<b>Link: </b>{data.LinkValue}");
        if (data.LinkMarkers is not null) builder.AppendLine($"<b>LinkMarkers: </b>{String.Join(", ", data.LinkMarkers.Select(AttributeHelper.GetEnumDescription))}");
        if (data.BanlistInfo is not null) {
            if (data.BanlistInfo.BanTcg is not null) builder.AppendLine($"<b>TCG Banlist: </b>{data.BanlistInfo.BanTcg}");
            if (data.BanlistInfo.BanOcg is not null) builder.AppendLine($"<b>OCG Banlist: </b>{data.BanlistInfo.BanOcg}");
        }
        builder.AppendLine();
        builder.AppendLine($"{data.Desc}");
        builder.AppendLine();
        if (data.Atk is not null) builder.AppendLine($"<b>ATK: </b>{data.Atk}");
        if (data.Def is not null) builder.AppendLine($"<b>DEF: </b>{data.Def}");
        if (data.Def is not null || data.Atk is not null) builder.AppendLine();
        if (data.Archetype is not null) {
            builder.AppendLine($"<b>Archetype: </b>{data.Archetype}");
            builder.AppendLine();
        }
        if (data.CardImages is not null && data.CardImages.Count > 0) {
            builder.Append($"<a href=\"{data.CardImages[0].ImageUrlCropped}\">CroppedImage</a> <a href=\"{data.CardImages[0].ImageUrl}\">FullImage</a> ");
        }
        builder.Append($"<a href=\"{data.YgoProDeckUrl}\">YgoProDeck</a>");
        //builder.AppendLine()
        return builder.ToString();
    }

    private static async Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult) {
        await Logging(botClient, $"""
                User: `{chosenInlineResult.From.Username} ({chosenInlineResult.From.FirstName} | {chosenInlineResult.From.LastName}) | {chosenInlineResult.From.Id}`
                Chosen: `{chosenInlineResult.ResultId}`
                Query: `{chosenInlineResult.Query}`
                """);
    }
}
