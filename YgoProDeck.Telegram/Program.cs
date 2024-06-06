using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

namespace YgoProDeck.Telegram {

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    public static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static async Task Main() {
            Console.WriteLine("Hello World!");
            TelegramBotClient bot = new();
            User me = await bot.GetMeAsync();
            using CancellationTokenSource cancellationSource = new();
            bot.StartReceiving(HandleUpdateAsync, PollingErrorHandler, null, cancellationSource.Token);
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            cancellationSource.Cancel();
        }

        private static async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct) {
            try {
                await (update.Type switch {
                    UpdateType.InlineQuery => BotOnInlineQueryReceived(bot, update.InlineQuery!), // Query
                    UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(bot, update.ChosenInlineResult!), // Feedback
                    _ => Task.CompletedTask,
                });
            }
            //#pragma warning disable CA1031
            catch (Exception ex) {
                Console.WriteLine($"Exception while handling {update.Type}: {ex}");
            }
            //#pragma warning restore CA1031
        }

        private static Task PollingErrorHandler(ITelegramBotClient bot, Exception ex, CancellationToken ct) {
            Console.WriteLine($"Exception while polling for updates: {ex}");
            return Task.CompletedTask;
        }


        private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery) {
            List<InlineQueryResult> results = [];
            Console.WriteLine(inlineQuery.Query);

            results.Add(
                new InlineQueryResultArticle(
                    "ID here",
                    inlineQuery.Query,
                    new InputTextMessageContent("Hello World" + inlineQuery.Query)
                    )
                );
            //Task.Delay(500).Wait();
            //await Task.Delay(500);
            Console.WriteLine("From: " + inlineQuery.From);
            Console.WriteLine("ID:" + inlineQuery.Id);

            await botClient.AnswerInlineQueryAsync(inlineQuery.Id, results);
        }

        private static async Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult) {
            //if (UInt32.TryParse(chosenInlineResult.ResultId, out UInt32 index)) {
            Console.WriteLine($"User {chosenInlineResult.From}. ID: {chosenInlineResult.ResultId}");
            //}
            //await botClient.SendTextMessageAsync(new ChatId(-743853037), "You have chosen " + chosenInlineResult.ResultId);
            await botClient.SendTextMessageAsync(new ChatId(),
                $"""
                User: `{chosenInlineResult.From}`
                Chosen: `{chosenInlineResult.ResultId}`
                Query: `{chosenInlineResult.Query}`
                """
                );
            //return Task.CompletedTask;
        }



    }
}
