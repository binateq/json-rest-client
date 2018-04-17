using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Binateq.JsonRestExtensions
{
    /// <summary>
    /// Extension methods that aid in making JSON REST requests using <see cref="HttpClient"/>.
    /// </summary>
    public static partial class JsonRestExtensions
    {
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
        };

        internal static HttpContent CreateJsonContent(object parameter)
        {
            var json = JsonConvert.SerializeObject(parameter, jsonSerializerSettings);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        internal static Uri BuildUri(FormattableString formattableString)
        {
            var uriFormatProvider = new UriFormatProvider();
            var uri = formattableString.ToString(uriFormatProvider);

            return new Uri(uri);
        }

        internal static Uri BuildUri(FormattableString formattableString, Dictionary<string, object> parameters)
        {
            var uriBuilder = new UriBuilder(BuildUri(formattableString));

            uriBuilder.Query = ToQueryString(uriBuilder.Query, parameters);

            return uriBuilder.Uri;
        }

        internal static Uri BuildUri(Uri baseUri, FormattableString formattableString)
        {
            var uriFormatProvider = new UriFormatProvider();
            var uri = formattableString.ToString(uriFormatProvider);

            if (string.IsNullOrWhiteSpace(uri))
                return baseUri;

            return new Uri(baseUri, uri);
        }

        internal static Uri BuildUri(Uri baseUri, FormattableString formattableString, Dictionary<string, object> parameters)
        {
            var uriBuilder = new UriBuilder(BuildUri(baseUri, formattableString));

            uriBuilder.Query = ToQueryString(uriBuilder.Query, parameters);

            return uriBuilder.Uri;
        }

        internal static async Task<T> ReadContentAsync<T>(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            var json = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
        }

        internal static async Task<HttpResponseMessage> ThrowIfInvalidStatusAsync(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            if (httpResponseMessage.IsSuccessStatusCode)
                return httpResponseMessage;

            throw new JsonRestException(httpResponseMessage.StatusCode, httpResponseMessage.Content);
        }

        internal static string ToQueryString(string initialQueryString, IReadOnlyDictionary<string, object> parameters)
        {
            var queryString = HttpUtility.ParseQueryString(initialQueryString);

            foreach (var parameter in parameters)
            {
                if (parameter.Value == null)
                    continue;

                if (parameter.Value is IEnumerable arrayParameters)
                {
                    foreach (var elementParameter in arrayParameters)
                        queryString.Add(parameter.Key, Format(elementParameter));
                }
                else
                    queryString.Add(parameter.Key, Format(parameter.Value));
            }

            return queryString.ToString();
        }

        private static string Format(object arg) => UriFormatProvider.FormatPrimitive(null, arg);
    }
}
