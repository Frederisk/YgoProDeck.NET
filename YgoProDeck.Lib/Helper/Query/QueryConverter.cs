using System;

namespace YgoProDeck.Lib.Helper.Query;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
internal class QueryConverterAttribute : Attribute {
    public QueryConverter Converter { get; init; }

    public String Name { get; init; }

    public QueryConverterAttribute(String name, Type converterType) {
        Name = name;
        Converter = Activator.CreateInstance(converterType) as QueryConverter ?? throw new ArgumentException($"Type {converterType.Name} is not a {nameof(QueryConverter)}");
    }

    public QueryConverterAttribute(String name) : this(name, typeof(ToStringConverter)) {
    }
}

internal abstract class QueryConverter {

    public abstract String? WriteValue(Object? value);
}

internal class ToStringConverter : QueryConverter {

    public override String? WriteValue(Object? value) {
        return value?.ToString();
    }
}
