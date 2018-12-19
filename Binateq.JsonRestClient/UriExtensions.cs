using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Binateq.JsonRestClient
{
    /// <summary>
    /// Implements methods to append tail to URI.
    /// </summary>
    internal static class UriExtensions
    {
        /// <summary>
        /// Appends path and possible query string to the end of the <paramref name="baseUri"/>.
        /// </summary>
        /// <param name="baseUri">Base URI.</param>
        /// <param name="tail">Tail of URI: path and possible query string.</param>
        /// <returns>Full URI.</returns>
        public static Uri Append(this Uri baseUri, FormattableString tail)
        {
            if (tail == null)
                throw new ArgumentNullException(nameof(tail));

            var stringUriTail = tail.ToString(new UriFormatProvider());

            return baseUri.Append(stringUriTail);
        }

        /// <summary>
        /// Appends path and possible query string to the end of the <paramref name="baseUri"/>.
        /// </summary>
        /// <param name="baseUri">Base URI.</param>
        /// <param name="tail">Tail of URI: path and possible query string.</param>
        /// <returns>Full URI.</returns>
        public static Uri Append(this Uri baseUri, string tail)
        {
            if (tail == null)
                throw new ArgumentNullException(nameof(tail));

            if (string.IsNullOrWhiteSpace(tail))
                return baseUri;

            var combinedQuery = new Uri(baseUri, tail);

            return AppendQuery(combinedQuery, baseUri.Query);
        }

        private static Uri AppendQuery(Uri uri, string query)
        {
            if (IsEmptyQuery(query))
                return uri;

            var uriBuilder = new UriBuilder(uri);
            if (IsEmptyQuery(uriBuilder.Query))
                uriBuilder.Query = RemoveStartingQuestion(query);
            else
                uriBuilder.Query = RemoveStartingQuestion(query) + "&" +
                                   RemoveStartingQuestion(uriBuilder.Query);

            return uriBuilder.Uri;
        }

        private static bool IsEmptyQuery(string query)
        {
            return query == "" || query == "?";
        }

        private static string RemoveStartingQuestion(string query)
        {
            if (query.StartsWith("?"))
                return query.Substring(1);

            return query;
        }

        /// <summary>
        /// Builds the URI from base URI and formattable string with placeholders,
        /// and query string parameters.
        /// </summary>
        /// <param name="baseUri">Base URI.</param>
        /// <param name="tail">Formattable string with placeholders.</param>
        /// <param name="isShortArraySerialization">See <see cref="JsonRestClientSettings.IsShortArraySerialization"/> for details.</param>
        /// <param name="queryStringParameters">Query string parameters.</param>
        /// <returns>Full URI.</returns>
        public static Uri Append(this Uri baseUri, FormattableString tail, bool isShortArraySerialization, IReadOnlyDictionary<string, object> queryStringParameters)
        {
            var uri = baseUri.Append(tail);
            var uriBuilder = new UriBuilder(uri);

            uriBuilder.Query = BuildQueryString(uriBuilder.Query, isShortArraySerialization, queryStringParameters);

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds query string from initial query string and dictionary of parameters.
        /// </summary>
        /// <param name="initialQueryString">Initial query string in form <c>p1=v1&p2=v2&p3=v3</c>. May be empty.</param>
        /// <param name="isShortArraySerialization">See <see cref="JsonRestClientSettings.IsShortArraySerialization"/> for details.</param>
        /// <param name="queryStringParameters">Dictionary of pairs, where each pair contains of
        /// <see cref="String">string</see> name and <see cref="Object">object</see> value.</param>
        /// <returns>Query string.</returns>
        internal static string BuildQueryString(string initialQueryString, bool isShortArraySerialization, IReadOnlyDictionary<string, object> queryStringParameters)
        {
            var queryString = MergeAndFormat(initialQueryString, queryStringParameters);

            if (isShortArraySerialization)
                return queryString.ToString();

            var assigns = queryString.AllKeys
                                     .SelectMany(queryString.GetValues,
                                                 (name, value) => name + '=' + value);

            return string.Join("&", assigns);
        }

        /// <summary>
        /// Merge initial query string and dictionary of parameters.
        /// </summary>
        /// <param name="initialQueryString">Initial query string in form <c>p1=v1&p2=v2&p3=v3</c>. May be empty.</param>
        /// <param name="queryStringParameters">Dictionary of pairs, where each pair contains of
        /// <see cref="String">string</see> name and <see cref="Object">object</see> value.</param>
        /// <returns>Collection that contains merged names and values.</returns>
        internal static NameValueCollection MergeAndFormat(string initialQueryString, IReadOnlyDictionary<string, object> queryStringParameters)
        {
            var queryString = HttpUtility.ParseQueryString(initialQueryString);

            return queryStringParameters.Where(x => x.Value != null)
                .Aggregate(queryString, AddAndFormat);
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