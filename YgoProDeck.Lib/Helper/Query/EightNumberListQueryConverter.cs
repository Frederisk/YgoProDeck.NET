using System;
using System.Collections.Generic;

namespace YgoProDeck.Lib.Helper.Query;
internal class EightNumberListQueryConverter : QueryConverter {
    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not IReadOnlyList<UInt64> numbers) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(IReadOnlyList<UInt64>).Name}");
        }
        return String.Join(",", numbers.ConvertAll(item => item.ToString("D8")));
    }
}
