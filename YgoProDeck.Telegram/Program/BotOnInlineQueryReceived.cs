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
                _ = Logging(botClient, "Url: " + requester.Uri); // Do not block execution.
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

    [GeneratedRegex(@"^\s*%\s*(\w*)\s*=\s*(.*?)\s*$", RegexOptions.Singleline)]
    private static partial Regex KeyValueRegex();

    private static (Dictionary<String, String> args, String? query) SplitQuery(String argsString) {
        IEnumerable<String> allQuery = argsString.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim());
        Dictionary<String, String> argsQuery = [];
        List<String> queries = [];
        foreach (String arg in allQuery) {
            var match = KeyValueRegex().Match(arg);
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
        AppendInfoString(builder, data.Type, "CardType", AttributeHelper.GetEnumDescription);
        String konami_id = data.Id.ToString("D8") + "/" + data.MiscInfo?.FirstOrDefault()?.KonamiId;
        AppendInfoString(builder, konami_id, "ID/KonamiID", i => i);
        AppendInfoString(builder, data.Attribute, "Attribute", i => i!.Value.ToString());
        AppendInfoString(builder, data.Race, "Race", i => i);
        AppendInfoString(builder, data.Level, "Level/Rank", i => i!.Value.ToString());
        AppendInfoString(builder, data.LinkValue, "Link", i => i!.Value.ToString());
        AppendInfoString(builder, data.LinkMarkers, "LinkMarkers", i => String.Join(", ", i.Select(AttributeHelper.GetEnumDescription)));
        AppendInfoString(builder, data.BanlistInfo?.BanTcg, "TCG Banlist", i => i!.Value.ToString());
        AppendInfoString(builder, data.BanlistInfo?.BanOcg, "OCG Banlist", i => i!.Value.ToString());
        MasterDuelRarity? md_rarity = data.MiscInfo?.FirstOrDefault()?.MDRarity;
        AppendInfoString(builder, md_rarity, "Master Duel Rarity", i => AttributeHelper.GetEnumDescription(i!.Value));

        builder.AppendLine();
        builder.AppendLine($"{data.Desc}");
        builder.AppendLine();
        if (data.Atk is not null) {
            String atk = data.Atk is -1 ? "?" : data.Atk.Value.ToString();
            builder.AppendLine($"<b>ATK: </b>{atk}");
        }
        if (data.Def is not null) {
            String def = data.Def is -1 ? "?" : data.Def.Value.ToString();
            builder.AppendLine($"<b>DEF: </b>{def}");
        }
        if (data.Def is not null || data.Atk is not null) builder.AppendLine();
        if (data.Archetype is not null) {
            builder.AppendLine($"<b>Archetype: </b>{data.Archetype}");
            builder.AppendLine();
        }
        if (data.CardImages is not null && data.CardImages.Count > 0) {
            builder.AppendLine($"<b>Images: </b>");
            foreach (var image in data.CardImages) {
                builder.AppendLine($"<a href=\"{image.ImageUrlCropped}\">CroppedImage</a> <a href=\"{image.ImageUrl}\">FullImage</a>");
            }
        }
        builder.AppendLine($"<b>Detail: </b><a href=\"{data.YgoProDeckUrl}\">YgoProDeck</a>");
        return builder.ToString();
    }

    private static void AppendInfoString<T>(StringBuilder builder, T? input, String key, Func<T, String> funcValue) {
        if (input is not null) {
            builder.Append($"<b>{key}: </b>{funcValue(input)}");
        }
    }

    private static async Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult) {
        await Logging(botClient, $"""
                User: `{chosenInlineResult.From.Username} ({chosenInlineResult.From.FirstName} | {chosenInlineResult.From.LastName}) | {chosenInlineResult.From.Id}`
                Chosen: `{chosenInlineResult.ResultId}`
                Query: `{chosenInlineResult.Query}`
                """);
    }
}
