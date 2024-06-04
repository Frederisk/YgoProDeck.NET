using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using YgoProDeck.Lib.EnumValue;
using YgoProDeck.Lib.Query;
using YgoProDeck.Lib.Response;

namespace YgoProDeck.Cli;

public class Program {
    public static async Task Main(String[] args) {
        //var json = """
        //    {"data":[{"id":21637210,"name":"Firewall Dragon Singularity","type":"Link Monster","frameType":"link","desc":"3+ Effect Monsters\r\n(Quick Effect): You can target cards your opponent controls or in their GY up to the number of different card types (Ritual, Fusion, Synchro, Xyz) you control and in your GY; return them to the hand, also this card gains 500 ATK for each returned card. If a monster this card points to is destroyed by battle, or sent to the GY: You can target 1 Cyberse monster in your GY; Special Summon it. You can only use each effect of \"Firewall Dragon Singularity\" once per turn.","atk":3500,"race":"Cyberse","attribute":"DARK","archetype":"Firewall","linkval":6,"linkmarkers":["Top-Left","Left","Bottom","Right","Top-Right","Top"],"ygoprodeck_url":"https://ygoprodeck.com/card/firewall-dragon-singularity-13603","card_sets":[{"set_name":"Cyberstorm Access","set_code":"CYAC-EN047","set_rarity":"Secret Rare","set_rarity_code":"(ScR)","set_price":"0"},{"set_name":"Cyberstorm Access","set_code":"CYAC-EN047","set_rarity":"Starlight Rare","set_rarity_code":"(StR)","set_price":"0"}],"card_images":[{"id":21637210,"image_url":"https://images.ygoprodeck.com/images/cards/21637210.jpg","image_url_small":"https://images.ygoprodeck.com/images/cards_small/21637210.jpg","image_url_cropped":"https://images.ygoprodeck.com/images/cards_cropped/21637210.jpg"}],"card_prices":[{"cardmarket_price":"6.99","tcgplayer_price":"2.48","ebay_price":"3.69","amazon_price":"0.00","coolstuffinc_price":"5.99"}],"misc_info":[{"views":53487,"viewsweek":156,"upvotes":0,"downvotes":0,"formats":["TCG","OCG"],"beta_id":101112047,"tcg_date":"2023-05-04","ocg_date":"2023-01-14","konami_id":18510,"has_effect":0}]},{"id":11738489,"name":"The Arrival Cyberse @Ignister","type":"Link Monster","frameType":"link","desc":"3+ monsters with different Attributes\r\nYou can only control 1 \"The Arrival Cyberse @Ignister\". The original ATK of this card becomes 1000 x the number of Link Materials used for its Link Summon. Unaffected by other cards' effects. Once per turn: You can target 1 other monster on the field; destroy it, and if you do, Special Summon 1 \"@Ignister Token\" (Cyberse/DARK/Level 1/ATK 0/DEF 0) to your zone this card points to.","atk":0,"race":"Cyberse","attribute":"DARK","archetype":"@Ignister","linkval":6,"linkmarkers":["Top","Left","Bottom-Left","Bottom","Bottom-Right","Right"],"ygoprodeck_url":"https://ygoprodeck.com/card/the-arrival-cyberse-ignister-10727","card_sets":[{"set_name":"Eternity Code","set_code":"ETCO-EN050","set_rarity":"Super Rare","set_rarity_code":"(SR)","set_price":"0"}],"card_images":[{"id":11738489,"image_url":"https://images.ygoprodeck.com/images/cards/11738489.jpg","image_url_small":"https://images.ygoprodeck.com/images/cards_small/11738489.jpg","image_url_cropped":"https://images.ygoprodeck.com/images/cards_cropped/11738489.jpg"}],"card_prices":[{"cardmarket_price":"1.71","tcgplayer_price":"0.46","ebay_price":"2.10","amazon_price":"0.25","coolstuffinc_price":"1.99"}],"misc_info":[{"views":219002,"viewsweek":78,"upvotes":22,"downvotes":4,"formats":["TCG","OCG"],"beta_id":101012050,"tcg_date":"2020-04-30","ocg_date":"2020-01-11","konami_id":15036,"has_effect":1}]}]}
        //    """;

        //var cardInfo = JsonSerializer.Deserialize<CardInfo>(json);

        //Console.WriteLine(cardInfo.Data[0].MiscInfo[0].HasEffect);




        //////////////////







        //Console.WriteLine("Hello, World!");

        //using HttpClient client = new HttpClient();



        Parameters parameters = new() {
            //Name = ["Dark Magician","Maxx \"C\""],
            //Name = ["QWERTYF"]
            LinkMarker = [LinkMarker.BottomLeft, LinkMarker.BottomRight],
            StartDate = DateOnly.Parse("2024-01-01"),
            //ID = [123123,1231,123,3],
            ////KonamiID = 017,
            ////ATK = (1000, ValueCompare.GreaterThan),
            ////ATK = 2500,
            ////D = 2100,
            ////Level = 7,
            //Type = [CardType.NormalMonster],
            //Race = [Race.Plant, Race.QuickPlay],
            Language = Language.French,
        };
        var uri = CardQuery.CreateQueryURI(parameters);
        Console.WriteLine(uri);

        //using HttpResponseMessage response = await client.GetAsync(uri);

        //if (!response.IsSuccessStatusCode) {
        //    var result = await response.Content.ReadFromJsonAsync<E>();
        //    Console.WriteLine(result?.Error);
        //    throw new HttpRequestException($"Failed to get card data. Status code: {response.StatusCode}");

        //}

        //String content = await response.Content.ReadAsStringAsync();

        //Console.WriteLine(content);
        //Console.WriteLine(GetPropertyDescription<Lib.Parameters>(nameof(Lib.Parameters.Name)));
        //Console.WriteLine(GetEnumDescription(Lib.ValueCompare.GreaterThan));
    }
}

