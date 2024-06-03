using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

using YgoProDeck.Lib.EnumValue;

namespace YgoProDeck.Lib;

public record class Parameters {
    [QueryConverter("name", typeof(CardNameListConverter))]
    public IReadOnlyList<String>? Name { get; init; }

    [QueryConverter("fname")]
    public String? FuzzyName { get; init; }

    [QueryConverter("id", typeof(EightNumberListConverter))]
    public IReadOnlyList<UInt64>? ID { get; init; }

    [QueryConverter("konami_id", typeof(NumberListConverter))]
    public IReadOnlyList<UInt64>? KonamiID { get; init; }

    [QueryConverter("type", typeof(EnumListDescriptionConverter<CardType>))]
    public IReadOnlyList<CardType>? Type { get; init; }

    [QueryConverter("atk", typeof(ComparableNumberConverter))]
    public (UInt64 Number, ValueCompare Compare)? ATK { get; init; }

    [QueryConverter("def", typeof(ComparableNumberConverter))]
    public (UInt64 Number, ValueCompare Compare)? DEF { get; init; }

    [QueryConverter("level", typeof(ComparableNumberConverter))]
    public (UInt64 Number, ValueCompare Compare)? Level { get; init; }

    [QueryConverter("race", typeof(EnumListDescriptionConverter<Race>))]
    public IReadOnlyList<Race>? Race { get; init; }

    [QueryConverter("attribute", typeof(EnumListDescriptionConverter<MonsterAttribute>))]
    public IReadOnlyList<MonsterAttribute>? Attribute { get; init; }

    [QueryConverter("link")]
    public UInt64? Link { get; init; }

    [QueryConverter("linkmarker", typeof(EnumListDescriptionConverter<LinkMarker>))]
    public IReadOnlyList<LinkMarker>? LinkMarker { get; init; } // Flags

    [QueryConverter("scale")]
    public UInt64? Scale { get; init; }

    [QueryConverter("cardset")]
    public String? CardSet { get; init; }

    [QueryConverter("archetype")]
    public String? ArcheType { get; init; }

    [QueryConverter("banlist", typeof(EnumDescriptionConverter))]
    public Banlist? Banlist { get; init; }

    [QueryConverter("sort", typeof(EnumDescriptionConverter))]
    public Sort? Sort { get; init; }

    [QueryConverter("format", typeof(EnumDescriptionConverter))]
    public Format? Format { get; init; }

    [QueryConverter("misc", typeof(YesOrNullConverter))]
    public Boolean Misc { get; init; } // Default: False

    [QueryConverter("staple", typeof(YesOrNullConverter))]
    public Boolean Staple { get; init; } // Default: False

    [QueryConverter("has_effect")]
    public Boolean? HasEffect { get; init; }

    [QueryConverter("startdate")]
    public DateOnly? StartDate { get; init; }

    [QueryConverter("enddate")]
    public DateOnly? EndDate { get; init; }

    [QueryConverter("dateregion", typeof(EnumDescriptionConverter))]
    public DateRegion? DateRegion { get; init; }

    [QueryConverter("language", typeof(LanguageConverter))]
    public Language Language { get; init; } // Default: English

    #region Converter

    protected class CardNameListConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not IReadOnlyList<String> names) {
                return null;
            }
            return String.Join("|", names);
        }
    }

    //protected class EightNumberConverter : QueryConverter {
    //    public override String? WriteValue(Object? value) {
    //        if (value is not UInt64 number) {
    //            return null;
    //        }
    //        return number.ToString("D8");
    //    }
    //}

    protected class EightNumberListConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not IReadOnlyList<UInt64> numbers) {
                return null;
            }
            return String.Join(",", numbers.ConvertAll(item => item.ToString("D8")));
        }
    }
    protected class NumberListConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not IReadOnlyList<UInt64> numbers) {
                return null;
            }
            return String.Join(",", numbers);
        }
    }

    protected class EnumDescriptionConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not Enum enumValue) {
                return null;
            }
            return GetEnumDescription(enumValue);
        }
    }

    protected class EnumListDescriptionConverter<T> : QueryConverter where T : Enum {
        public override String? WriteValue(Object? value) {
            if (value is not IReadOnlyList<T> enums) {
                return null;
            }
            return String.Join(",", enums.ConvertAll(item => GetEnumDescription(item)));
        }
    }

    protected class ComparableNumberConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not ValueTuple<UInt64, ValueCompare>(UInt64 Number, ValueCompare Compare)) {
                return null;
            }
            return $"{GetEnumDescription(Compare)}{Number}";
        }
    }

    protected class YesOrNullConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not Boolean boolean) {
                return null;
            }
            return boolean ? "yes" : null;
        }
    }

    protected class LanguageConverter : QueryConverter {
        public override String? WriteValue(Object? value) {
            if (value is not Language language) {
                return null;
            }
            var description = GetEnumDescription(language);
            return String.IsNullOrEmpty(description) ? null : description;
        }
    }

    protected static String GetEnumDescription<T>(T value) where T : Enum {
        //ArgumentNullException.ThrowIfNull(value);
        String name = Enum.GetName(value.GetType(), value) ?? throw new NullReferenceException($"Name not found for {value}");
        FieldInfo field = value.GetType().GetField(name) ?? throw new NullReferenceException($"Field {name} not found in {value.GetType().Name}");
        DescriptionAttribute attribute = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute ?? throw new NullReferenceException($"{nameof(DescriptionAttribute)} not found in {name}");
        return attribute.Description;
    }

    //private static String GetPropertyDescription<T>(String propertyName) {
    //    ArgumentNullException.ThrowIfNull(propertyName);
    //    PropertyInfo property = typeof(T).GetProperty(propertyName) ?? throw new NullReferenceException($"Property {propertyName} not found in {typeof(T).Name}");
    //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute ?? throw new NullReferenceException($"{nameof(DescriptionAttribute)} not found in {propertyName}");
    //    return attribute!.Description;
    //}

    #endregion Converter
}

internal static class IReadOnlyListExtensions {

    public static IReadOnlyList<TOutput> ConvertAll<T, TOutput>(this IReadOnlyList<T> sources, Converter<T, TOutput> converter) {
        List<TOutput> results = new(sources.Count);
        foreach (T source in sources) {
            results.Add(converter(source));
        }
        return results;
    }
}
