using System;
using System.Collections.Generic;

namespace YgoProDeck.Lib.Helper.Query;

internal class CardNameListQueryConverter : QueryConverter {

    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not IReadOnlyList<String> names) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(IReadOnlyList<String>).Name}");
        }
        return String.Join("|", names);
    }
}
