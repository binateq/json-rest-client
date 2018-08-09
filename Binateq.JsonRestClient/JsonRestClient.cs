using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Binateq.JsonRestClient
{
    /// <summary>
    /// Extension methods that aid in making JSON REST requests using <see cref="HttpClient"/>.
    /// </summary>
    public partial class JsonRestClient
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly JsonRestClientSettings settings;

        /// <summary>
        /// Initializes the instance of <see cref="JsonRestClient"/> type with specified settings.
        /// </summary>
        public JsonRestClient(HttpClient httpClient, Uri baseUri, JsonRestClientSettings settings)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.baseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));

            ThrowIfInvalidSettings();
        }

        private void ThrowIfInvalidSettings()
        {
            if (settings.Serialize == null)
                throw new ArgumentNullException(nameof(settings.Serialize));

            if (settings.Deserialize == null)
                throw new ArgumentNullException(nameof(settings.Deserialize));
        }

        /// <summary>
        /// Initializes the instance of <see cref="JsonRestClient"/> type with default settings.
        /// </summary>
        public JsonRestClient(HttpClient httpClient, Uri baseUri)
            : this(httpClient, baseUri, new JsonRestClientSettings())
        { }

        /// <summary>
        /// Creates <see cref="HttpContent"/> instance containg JSON represenation of <paramref name="contentParameter"/>.
        /// </summary>
        /// <param name="contentParameter">Data transfer object to serialize to JSON.</param>
        /// <returns>HTTP content containing serialized parameter.</returns>
        protected internal virtual HttpContent CreateJsonContent(object contentParameter)
        {
            var json = settings.Serialize(contentParameter);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// Builds the URI from base URI and formattable string with placeholders.
        /// </summary>
        /// <param name="formattableString">Formattable string with placeholders.</param>
        /// <returns>Full URI.</returns>
        protected internal virtual Uri BuildUri(FormattableString formattableString)
        {
            var uri = formattableString.ToString(new UriFormatProvider());

            if (string.IsNullOrWhiteSpace(uri))
                return baseUri;

            var uriBuilder = new UriBuilder(new Uri(baseUri, uri));

            // https://msdn.microsoft.com/en-us/library/system.uribuilder.query.aspx
            var baseUriQuery = Regex.Replace(baseUri.Query, @"^\?+", "");

            if (uriBuilder.Query.Length > 1)
                uriBuilder.Query = uriBuilder.Query.Substring(1) + "&" + baseUriQuery;
            else
                uriBuilder.Query = baseUriQuery;

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds the URI from base URI and formattable string with placeholders,
        /// and query string parameters.
        /// </summary>
        /// <param name="formattableString">Formattable string with placeholders.</param>
        /// <param name="queryStringParameters">Query string parameters.</param>
        /// <returns>Full URI.</returns>
        protected internal virtual Uri BuildUri(FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters)
        {
            var uri = BuildUri(formattableString);
            var uriBuilder = new UriBuilder(uri);

            uriBuilder.Query = BuildQueryString(uriBuilder.Query, queryStringParameters);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds query string from initial query string and dictionary of parameters.
        /// </summary>
        /// <param name="initialQueryString">Initial query string in form <c>p1=v1&p2=v2&p3=v3</c>. May be empty.</param>
        /// <param name="queryStringParameters">Dictionary of pairs, where each pair contains of
        /// <see cref="String">string</see> name and <see cref="Object">object</see> value.</param>
        /// <returns>Query string.</returns>
        protected internal virtual string BuildQueryString(string initialQueryString, IReadOnlyDictionary<string, object> queryStringParameters)
        {
            var queryString = MergeAndFormat(initialQueryString, queryStringParameters);

            if (settings.IsShortArraySerialization)
                return queryString.ToString();

            var assigns = queryString.AllKeys
                                     .SelectMany(queryString.GetValues,
                                                 (name, value) => name + '=' + value);

            return string.Join("&", assigns);
        }

        /// <summary>
        /// Merge initial query string and dictionary of parameteres.
        /// </summary>
        /// <param name="initialQueryString">Initial query string in form <c>p1=v1&p2=v2&p3=v3</c>. May be empty.</param>
        /// <param name="queryStringParameters">Dictionary of pairs, where each pair contains of
        /// <see cref="String">string</see> name and <see cref="Object">object</see> value.</param>
        /// <returns>Collection that contains merged names and values.</returns>
        protected internal virtual NameValueCollection MergeAndFormat(string initialQueryString, IReadOnlyDictionary<string, object> queryStringParameters)
        {
            var queryString = HttpUtility.ParseQueryString(initialQueryString);

            return queryStringParameters.Where(x => x.Value != null)
                                        .Aggregate(queryString, AddAndFormat);
        }

        /// <summary>
        /// Configures <paramref name="task"/> to run to free context and ignores all exceptions.
        /// </summary>
        /// <param name="task">Task to forget.</param>
        /// <returns>Task.</returns>
        protected internal async Task ForgetAsync(Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
                // ignore
            }
        }

        private static NameValueCollection AddAndFormat(NameValueCollection queryString, KeyValuePair<string, object> parameter)
        {
            if (parameter.Value is IEnumerable array && parameter.Value.GetType() != typeof(string))
            {
                foreach (var element in array)
                    queryString.Add(parameter.Key, UriFormatProvider.FormatAndEscape(string.Empty, element));
            }
            else
                queryString.Add(parameter.Key, UriFormatProvider.FormatAndEscape(string.Empty, parameter.Value));

            return queryString;
        }
    }
}
