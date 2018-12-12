using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Binateq.JsonRestClient
{
    /// <summary>
    /// Implements helper methods for <see cref="Task{T}"/> containing <see cref="HttpResponseMessage"/>.
    /// </summary>
    internal static class HttpResponseMessageTaskExtensions
    {
        /// <summary>
        /// Reads and deserializes content from <see cref="HttpResponseMessage"/>.
        /// </summary>
        /// <typeparam name="T">Type of deserialized object.</typeparam>
        /// <param name="httpResponseMessageTask"><see cref="Task{T}"/> containing <see cref="HttpResponseMessage"/>.</param>
        /// <param name="deserialize">Function of deserialization.</param>
        /// <returns>Deserialized object.</returns>
        public static async Task<T> ReadAsync<T>(this Task<HttpResponseMessage> httpResponseMessageTask,
            Func<string, Type, object> deserialize)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return (T)deserialize(content, typeof(T));
        }

        /// <summary>
        /// Reads and deserializes content from <see cref="HttpResponseMessage"/>
        /// or returns <c>default(<typeparamref name="T"/>)</c> if HTTP status is <see cref="HttpStatusCode.NotFound"/>
        /// or throws the <see cref="JsonRestException"/> otherwise.
        /// </summary>
        /// <typeparam name="T">Type of deserialized object.</typeparam>
        /// <param name="httpResponseMessageTask"><see cref="Task{T}"/> containing <see cref="HttpResponseMessage"/>.</param>
        /// <param name="deserialize">Function of deserialization.</param>
        /// <returns>Deserialized object.</returns>
        public static async Task<T> ReadOrDefaultOrThrowAsync<T>(this Task<HttpResponseMessage> httpResponseMessageTask,
            Func<string, Type, object> deserialize)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                return default(T);

            var uri = httpResponseMessage.RequestMessage.RequestUri;
            var requestContent = await httpResponseMessage.RequestMessage.Content.ReadAsStringAsync();
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = httpResponseMessage.StatusCode;

            if (httpResponseMessage.IsSuccessStatusCode)
                return (T)deserialize(responseContent, typeof(T));

            throw new JsonRestException(uri, requestContent, responseContent, statusCode);
        }

        /// <summary>
        /// Throws <see cref="JsonRestException"/> if <see cref="HttpResponseMessage"/> has invalid status.
        /// </summary>
        /// <param name="httpResponseMessageTask"><see cref="Task{T}"/> containing <see cref="HttpResponseMessage"/>.</param>
        /// <returns><paramref name="httpResponseMessageTask"/>, if status is valid.</returns>
        public static async Task<HttpResponseMessage> ThrowIfInvalidStatusAsync(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            if (httpResponseMessage.IsSuccessStatusCode)
                return httpResponseMessage;

            var uri = httpResponseMessage.RequestMessage.RequestUri;
            var requestContent = await ReadStringContentOrNullAsync(httpResponseMessage.RequestMessage.Content);
            var responseContent = await ReadStringContentOrNullAsync(httpResponseMessage.Content);
            var statusCode = httpResponseMessage.StatusCode;

            throw new JsonRestException(uri, requestContent, responseContent, statusCode);
        }

        private static async Task<string> ReadStringContentOrNullAsync(this HttpContent content)
        {
            if (content == null)
                return null;

            return await content.ReadAsStringAsync();
        }
    }
}
