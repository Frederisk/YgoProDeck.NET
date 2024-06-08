using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Helper;
using YgoProDeck.Lib.Query;
using YgoProDeck.Lib.Response;

namespace YgoProDeck.Telegram {

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static async Task Main() {
            String? token = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
            if (String.IsNullOrWhiteSpace(token)) {
                Console.WriteLine("TELEGRAM_BOT_TOKEN is not set");
                return;
            }
            TelegramBotClient bot = new(token);
            User me = await bot.GetMeAsync();
            using CancellationTokenSource cancellationSource = new();
            bot.StartReceiving(HandleUpdateAsync, PollingErrorHandler, null, cancellationSource.Token);
            //Console.WriteLine($"Start listening for @{me.Username}");
            await Logging(bot, $"Start listening for @{me.Username}");
            while (true) {
                var exit = Console.ReadLine();
                if (exit == "exit") {
                    cancellationSource.Cancel();
                    break;
                }
            }
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct) {
            try {
                await (update.Type switch {
                    UpdateType.InlineQuery => BotOnInlineQueryReceived(bot, update.InlineQuery!), // Query
                    UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(bot, update.ChosenInlineResult!), // Feedback
                    // UpdateType.ChatMember => Logging(bot, $"ChatMember: {update.ChatMember}"),
                    UpdateType.Message => BotOnMessageReceived(bot, update.Message!), // Message
                    _ => Task.CompletedTask,
                });
            } catch (Exception ex) {
                await Logging(bot, $"Exception while handling {update.Type}: {ex}");
            }
        }

        private static async Task PollingErrorHandler(ITelegramBotClient bot, Exception ex, CancellationToken ct) {
            await Logging(bot, $"Exception while polling for updates: {ex}");
            return;
        }

        private static readonly SemaphoreSlim semaphore = new(1, 1);

        private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery) {
            if (String.IsNullOrWhiteSpace(inlineQuery.Query)) {
                await botClient.AnswerInlineQueryAsync(inlineQuery.Id, []);
                return;
            }

            var allQuery = inlineQuery.Query.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim());
            Dictionary<String, String> argsQuery = [];
            List<String> queries = [];
            foreach (var arg in allQuery) {
                var match = Regex.Match(arg, @"^\s*%\s*(\w*)\s*=\s*(.*?)\s*$", RegexOptions.Singleline);
                if (match.Success) {
                    argsQuery.Add(match.Groups[1].Value, match.Groups[2].Value.Trim());
                } else {
                    queries.Add(arg);
                }
            }

            if (queries.Count == 0) {
                await botClient.AnswerInlineQueryAsync(inlineQuery.Id, []);
                return;
            }

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

            QueryParameters par = new() {
                FuzzyName = queries.First(),
                Language = langEnum,
                Offset = 0,
                Number = 15,
                Misc = true,
            };

            List<InlineQueryResult> results = [];

            if (await semaphore.WaitAsync(TimeSpan.FromMilliseconds(200))) {
                //Console.WriteLine("Start");
                try {
                    // Web
                    CardInfoRequester requester = new(par);
                    CardInfo a = (await requester.RequestAsync())!;

                    foreach (var data in a.Data) {
                        StringBuilder builder = new();
                        builder.AppendLine($"<b>{data.Name}</b>");
                        builder.AppendLine();
                        builder.AppendLine($"<b>CardType: </b>{data.Type}");
                        builder.AppendLine($"<b>ID/KonamiID: </b>{data.Id}/{data.MiscInfo?.FirstOrDefault()?.KonamiId}");
                        if (data.Attribute is not null) builder.AppendLine($"<b>Attribute: </b>{data.Attribute}");
                        if (data.Race is not null) builder.AppendLine($"<b>Race: </b>{data.Race}");
                        if (data.Level is not null) builder.AppendLine($"<b>Level/Rank: </b>{data.Level}");
                        if (data.LinkValue is not null) builder.AppendLine($"<b>Link: </b>{data.LinkValue}");
                        if (data.LinkMarkers is not null) builder.AppendLine($"<b>LinkMarkers: </b>{String.Join(", ", data.LinkMarkers.Select(AttributeHelper.GetEnumDescription))}");
                        builder.AppendLine();
                        builder.AppendLine($"{data.Desc}");
                        builder.AppendLine();
                        if (data.Atk is not null) builder.AppendLine($"<b>ATK: </b>{data.Atk}");
                        if (data.Def is not null) builder.AppendLine($"<b>DEF: </b>{data.Def}");
                        if (data.Archetype is not null) builder.AppendLine($"<b>Archetype: </b>{data.Archetype}");
                        builder.AppendLine();
                        if (data.CardImages is not null && data.CardImages.Count > 0) {
                            builder.Append($"<a href=\"{data.CardImages[0].ImageUrlCropped}\">CroppedImage</a> <a href=\"{data.CardImages[0].ImageUrl}\">FullImage</a> ");
                        }
                        builder.Append($"<a href=\"{data.YgoProDeckUrl}\">YgoProDeck</a>");
                        //builder.AppendLine()

                        results.Add(
                            new InlineQueryResultArticle(
                                $"{data.Name} || {data.Id:D8}",
                                data.Name,
                                new InputTextMessageContent(builder.ToString()) {
                                    ParseMode = ParseMode.Html,
                                }
                            )
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

        private static async Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult) {
            await Logging(botClient, $"""
                User: `{chosenInlineResult.From.Username} ({chosenInlineResult.From.FirstName} | {chosenInlineResult.From.LastName}) | {chosenInlineResult.From.Id}`
                Chosen: `{chosenInlineResult.ResultId}`
                Query: `{chosenInlineResult.Query}`
                """);
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message) {
            if (message.Type != MessageType.Text) {
                return;
            }

            if (message.Text is null) {
                return;
            }
            await botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: """
                Hello, I'm @InYgoProDeckBot!
                I can help you to search for Yu-Gi-Oh! cards.

                Currently, I can only search card in inline mode. You only need to write the card name in your input box like this:

                `@InYgoProDeckBot Maxx "C"`

                I will show you the details of the card.

                For more information, you can visit <https://github.com/Frederisk/YgoProDeck.NET>
                """,
                parseMode: ParseMode.Markdown
                );

            // if (message.Text.StartsWith("/start")) {
            //     await botClient.SendTextMessageAsync(
            //         chatId: message.Chat,
            //         text: "Hello, I'm YgoProDeckBot. I can help you to search for Yu-Gi-Oh! cards. Just type the card name and I will show you the details.");
            //     return;
            // }

            // if (message.Text.StartsWith("/help")) {
            //     await botClient.SendTextMessageAsync(
            //         chatId: message.Chat,
            //         text: "Just type the card name and I will show you the details.");
            //     return;
            // }

            // if (message.Text.StartsWith("/")) {
            //     await botClient.SendTextMessageAsync(
            //         chatId: message.Chat,
            //         text: "Unknown command. Type /help for help."
            //         );
            //     return;
            // }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static async Task Logging(ITelegramBotClient botClient, String message) {
            try {
                await botClient.SendTextMessageAsync(
                    new ChatId(-743853037),
                    message);
            }
            catch (Exception ex) {
                Console.WriteLine($"Exception while logging: {ex}");
            }
        }
    }
}
