using System;

using YgoProDeck.Lib.EnumValue;

namespace YgoProDeck.Lib.Helper.Query;

internal class ComparableNumberQueryConverter : QueryConverter {

    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not ValueTuple<UInt64, ValueCompare>(UInt64 Number, ValueCompare Compare)) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(ValueTuple<UInt64, ValueCompare>).Name}");
        }
        return $"{AttributeHelper.GetEnumDescription(Compare)}{Number}";
    }
}
