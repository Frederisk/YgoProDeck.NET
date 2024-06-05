using System;

namespace YgoProDeck.Lib.Helper.Query;

internal class EnumDescriptionQueryConverter : QueryConverter {

    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not Enum enumValue) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(Enum).Name}");
        }
        return AttributeHelper.GetEnumDescription(enumValue);
    }
}
