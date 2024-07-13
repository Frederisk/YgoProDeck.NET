# YgoProDeck.NET

The data source of this library is [YgoProDeck](https://ygoprodeck.com/api-guide/), and the information is obtained by making requests through the API. Since the YgoProDeck site is not under my maintenance, please tell me any program execution errors, and for data content errors, please inform the site owner (or tell me and let me forward it).

The warehouse is mainly divided into two parts, YgoProDeck.Lib and YgoProDeck.Telegram. The former is what most people need and is a wrapper for the YgoProDeck API. The latter is a simple example, that is, a card query robot [@InYgoProDeckBot](https://t.me/InYgoProDeckBot) on Telegram.

## YgoProDeck.Lib

How to use it is simple:

```cs
using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Helper;
using YgoProDeck.Lib.Query;
using YgoProDeck.Lib.Response;

// Query
QueryParameters par = new() {
  FuzzyName = @"MAXX",
  Language = Language.English,
  Offset = 0,
  Number = 15,
  Misc = true,
};
// Request
CardInfoRequester requester = new(par);
try {
CardInfo? = await requester.RequestAsync();
} catch (HttpRequestException e) { // Web Error or Card Not Found
  /* ignore */
}
// Procress
Console.WriteLine(data?.Name)
```

## YgoProDeck.Telegram

This is an Inline Bot, so you only need to enter `@InYgoProDeckBot <CardName>` in the message sending field, and then select your card in the pop-up menu.

For example:

![image](https://github.com/user-attachments/assets/ef6fb86d-5e35-4f1f-b02b-525c3dcf8e6a)
