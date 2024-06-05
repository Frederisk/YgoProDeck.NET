using System;

using YgoProDeck.Lib.EnumValue;

namespace YgoProDeck.Lib.Helper.Query;

internal class LanguageQueryConverter : QueryConverter {

    public override String? WriteValue(Object? value) {
        if (value is null) { return null; }
        if (value is not Language language) {
            throw new ArgumentException($"Invalid type {value.GetType().Name}, expected {typeof(Language).Name}");
        }
        String description = AttributeHelper.GetEnumDescription(language);
        return String.IsNullOrEmpty(description) ? null : description;
    }
}
