using System;
using System.Collections.Generic;

namespace YgoProDeck.Lib.Helper.Query;

internal class EnumListDescriptionQueryConverter<T> : QueryConverter where T : Enum {

    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not IReadOnlyList<T> enums) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(IReadOnlyList<T>).Name}");
        }
        return String.Join(",", enums.ConvertAll(item => AttributeHelper.GetEnumDescription(item)));
    }
}
