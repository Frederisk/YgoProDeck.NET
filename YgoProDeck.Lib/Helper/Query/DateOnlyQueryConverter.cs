using System;

namespace YgoProDeck.Lib.Helper.Query;
internal class DateOnlyQueryConverter : QueryConverter {
    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not DateOnly date) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(DateOnly).Name}");
        }
        return date.ToString("yyyy-MM-dd");
    }
}
