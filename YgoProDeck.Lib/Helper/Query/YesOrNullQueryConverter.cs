using System;

namespace YgoProDeck.Lib.Helper.Query;

internal class YesOrNullQueryConverter : QueryConverter {
    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not Boolean boolean) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(Boolean).Name}");
        }
        return boolean ? "yes" : null;
    }
}