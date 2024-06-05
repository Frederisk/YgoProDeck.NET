using System;
using System.ComponentModel;
using System.Reflection;

namespace YgoProDeck.Lib.Helper;

public static class AttributeHelper {

    public static String GetEnumDescription<T>(T value) where T : Enum {
        //ArgumentNullException.ThrowIfNull(value);
        String name = Enum.GetName(value.GetType(), value) ?? throw new NullReferenceException($"Name not found for {value}");
        FieldInfo field = value.GetType().GetField(name) ?? throw new NullReferenceException($"Field {name} not found in {value.GetType().Name}");
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute ?? throw new NullReferenceException($"{nameof(DescriptionAttribute)} not found in {name}");
        return attribute.Description;
    }

    //public static String GetPropertyDescription<T>(String propertyName) {
    //    ArgumentNullException.ThrowIfNull(propertyName);
    //    PropertyInfo property = typeof(T).GetProperty(propertyName) ?? throw new NullReferenceException($"Property {propertyName} not found in {typeof(T).Name}");
    //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute ?? throw new NullReferenceException($"{nameof(DescriptionAttribute)} not found in {propertyName}");
    //    return attribute!.Description;
    //}
}
