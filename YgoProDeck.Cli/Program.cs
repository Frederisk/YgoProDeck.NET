using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using YgoProDeck.Lib.Query;

namespace YgoProDeck.Cli;

public class Program {
    public static async Task Main(String[] args) {
        Console.WriteLine("Hello, World!");

        using HttpClient client = new HttpClient();



        Parameters parameters = new() {
            //Name = ["Dark Magician","Maxx \"C\""],
            Name = ["QWERTYF"]
            //ID = [123123,1231,123,3],
            ////KonamiID = 017,
            ////ATK = (1000, ValueCompare.GreaterThan),
            ////ATK = 2500,
            ////D = 2100,
            ////Level = 7,
            //Type = [CardType.NormalMonster],
            //Race = [Race.Plant, Race.QuickPlay],
            //Language = Language.English,
        };
        var uri = CardQuery.CreateQueryURI(parameters);
        using HttpResponseMessage response = await client.GetAsync(uri);

        if (!response.IsSuccessStatusCode) {
            var result = await response.Content.ReadFromJsonAsync<E>();
            Console.WriteLine(result?.Error);
            throw new HttpRequestException($"Failed to get card data. Status code: {response.StatusCode}");

        }

        String content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content);
        //Console.WriteLine(GetPropertyDescription<Lib.Parameters>(nameof(Lib.Parameters.Name)));
        //Console.WriteLine(GetEnumDescription(Lib.ValueCompare.GreaterThan));
    }

    private class E {
        [JsonPropertyName("error")]
        public String Error { get; set; }
    }
    //public static String GetPropertyDescription<T>(String propertyName) {
    //    ArgumentNullException.ThrowIfNull(propertyName);
    //    PropertyInfo property = typeof(T).GetProperty(propertyName) ?? throw new NullReferenceException($"Property {propertyName} not found in {typeof(T).Name}");
    //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)) as DescriptionAttribute ?? throw new NullReferenceException($"{nameof(DescriptionAttribute)} not found in {propertyName}");
    //    return attribute!.Description;
    //}

    //public static String GetEnumDescription<T>(T value) where T: Enum {
    //    ArgumentNullException.ThrowIfNull(value);
    //    String name = Enum.GetName(typeof(T), value) ?? throw new NullReferenceException($"Name not found for {value}");
    //    FieldInfo field = typeof(T).GetField(name) ?? throw new NullReferenceException($"Field {name} not found in {typeof(T).Name}");
    //    DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute ?? throw new NullReferenceException($"{nameof(DescriptionAttribute)} not found in {name}");
    //    return attribute!.Description;
    //}
}

