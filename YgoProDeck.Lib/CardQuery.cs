using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;

namespace YgoProDeck.Lib;

public static class CardQuery {
    public static readonly String BaseUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";

    public static String? CreateQueryURI(Parameters pars) {
        UriBuilder uri = new(BaseUrl);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

        foreach (PropertyInfo property in pars.GetType().GetProperties()) {
            var queryConverterAttributes = property.GetCustomAttribute<QueryConverterAttribute>();
            if (queryConverterAttributes is null) {
                continue;
            }

            var propertyValue = property.GetValue(pars);
            if (propertyValue is null) {
                continue;
            }

            var realValue = queryConverterAttributes.Converter.WriteValue(propertyValue);
            if (realValue is null) {
                continue;
            }

            query.Add(queryConverterAttributes.Name, realValue);
            //query[queryConverterAttributes.Name] = queryConverterAttributes.Converter.WriteValue(propertyValue);
        }
        uri.Query = query.ToString();

        return uri.ToString();
    }
}
