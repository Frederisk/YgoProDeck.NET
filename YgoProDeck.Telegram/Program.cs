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
    public static partial class Program {

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
            await Logging(bot, $"Start listening for @{me.Username}");
            while (true) {
                var exit = Console.ReadLine();
                if (exit is "exit") {
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static async Task Logging(ITelegramBotClient botClient, String message) {
            try {
                await botClient.SendTextMessageAsync(
                    new ChatId(-743853037),
                    message);
            } catch (Exception ex) {
                Console.WriteLine($"Exception while logging: {ex}");
            }
        }
    }
}
