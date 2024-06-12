using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace YgoProDeck.Telegram;

public static partial class Program {

    private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message) {
        if (message.Type != MessageType.Text) {
            return;
        }

        if (message.Text is null) {
            return;
        }

        if (message.Text.StartsWith("/start")) {
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
        }

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
}
