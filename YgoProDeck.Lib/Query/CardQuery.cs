using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;

namespace YgoProDeck.Lib.Query;

public static class CardQuery {
    public static readonly String BaseUrl = "https://db.ygoprodeck.com/api/v7/cardinfo.php";

    public static Uri? CreateQueryURI(Parameters pars) {
        UriBuilder uri = new(BaseUrl);
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

        foreach (PropertyInfo property in pars.GetType().GetProperties()) {
            QueryConverterAttribute? attribute = property.GetCustomAttribute<QueryConverterAttribute>();
            if (attribute is null) {
                continue;
            }

            Object? propertyValue = property.GetValue(pars);
            if (propertyValue is null) {
                continue;
            }

            String? realValue = attribute.Converter.WriteValue(propertyValue);
            if (realValue is null) {
                continue;
            }

            query.Add(attribute.Name, realValue);
            //query[queryConverterAttributes.Name] = queryConverterAttributes.Converter.WriteValue(propertyValue);
        }
        uri.Query = query.ToString();

        return uri.Uri;
    }
}
