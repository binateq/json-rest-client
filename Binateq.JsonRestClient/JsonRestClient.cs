using System;
using System.Net.Http;
using System.Text;

namespace Binateq.JsonRestClient
{
    /// <summary>
    /// Extension methods that aid in making JSON REST requests using <see cref="System.Net.Http.HttpClient"/>.
    /// </summary>
    public partial class JsonRestClient
    {
        private readonly Func<HttpClient> _getHttpClient;
        private readonly Uri _baseUri;
        private readonly JsonRestClientSettings _settings;

        /// <summary>
        /// Gets the current <c>HttpClient</c> instance.
        /// </summary>
        protected HttpClient HttpClient => _getHttpClient();

        private JsonRestClient(Uri baseUri, JsonRestClientSettings settings)
        {
            _baseUri = baseUri ?? throw new ArgumentNullException(nameof(baseUri));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            ThrowIfInvalidSettings();
        }

        /// <summary>
        /// Initializes the instance of <see cref="JsonRestClient"/> type with specified settings.
        /// </summary>
        public JsonRestClient(HttpClient httpClient, Uri baseUri, JsonRestClientSettings settings)
            : this(baseUri, settings)
        {
            if (httpClient == null)
                throw new ArgumentNullException(nameof(httpClient));

            _getHttpClient = () => httpClient;
        }

        /// <summary>
        /// Initializes the instance of <see cref="JsonRestClient"/> type with default settings.
        /// </summary>
        public JsonRestClient(HttpClient httpClient, Uri baseUri)
            : this(httpClient, baseUri, JsonRestClientSettings.Default)
        { }

        /// <summary>
        /// Initializes the instance of <see cref="JsonRestClient"/> type with specified settings.
        /// </summary>
        public JsonRestClient(IHttpClientFactory httpClientFactory, Uri baseUri, JsonRestClientSettings settings)
            : this(baseUri, settings)
        {
            if (httpClientFactory == null)
                throw new ArgumentNullException(nameof(httpClientFactory));

            _getHttpClient = httpClientFactory.CreateClient;
        }

        /// <summary>
        /// Initializes the instance of <see cref="JsonRestClient"/> type with specified settings.
        /// </summary>
        public JsonRestClient(IHttpClientFactory httpClientFactory, Uri baseUri)
            : this(httpClientFactory, baseUri, JsonRestClientSettings.Default)
        { }

        private void ThrowIfInvalidSettings()
        {
            if (_settings.Serialize == null)
                throw new ArgumentNullException(nameof(_settings.Serialize));

            if (_settings.Deserialize == null)
                throw new ArgumentNullException(nameof(_settings.Deserialize));
        }

        /// <summary>
        /// Creates <see cref="HttpContent"/> instance containing JSON representation of <paramref name="dto"/>.
        /// </summary>
        /// <param name="dto">Data Transfer Object to serialize to JSON.</param>
        /// <returns>HTTP content containing serialized Data Transfer Object.</returns>
        protected internal virtual HttpContent CreateJsonContent(object dto)
        {
            var json = _settings.Serialize(dto);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
