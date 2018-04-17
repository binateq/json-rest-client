using System;
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
        public static async Task<T> ReadContentAsync<T>(this Task<HttpResponseMessage> httpResponseMessageTask,
            Func<string, Type, object> deserialize)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            var json = await httpResponseMessage.Content.ReadAsStringAsync();

            return (T)deserialize(json, typeof(T));
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

            throw new JsonRestException(httpResponseMessage.StatusCode, httpResponseMessage.Content);
        }
    }
}
