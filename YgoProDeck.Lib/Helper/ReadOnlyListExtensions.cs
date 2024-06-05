using System;
using System.Collections.Generic;

namespace YgoProDeck.Lib.Helper;
internal static class ReadOnlyListExtensions {

    public static IReadOnlyList<TOutput> ConvertAll<T, TOutput>(this IReadOnlyList<T> sources, Converter<T, TOutput> converter) {
        List<TOutput> results = new(sources.Count);
        foreach (T source in sources) {
            results.Add(converter(source));
        }
        return results;
    }
}
