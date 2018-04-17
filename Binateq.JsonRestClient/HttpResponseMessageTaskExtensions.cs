using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Binateq.JsonRestClient
{
    internal static class HttpResponseMessageTaskExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this Task<HttpResponseMessage> httpResponseMessageTask,
            Func<string, Type, object> jsonDeserialize)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            var json = await httpResponseMessage.Content.ReadAsStringAsync();

            return (T)jsonDeserialize(json, typeof(T));
        }

        public static async Task<HttpResponseMessage> ThrowIfInvalidStatusAsync(this Task<HttpResponseMessage> httpResponseMessageTask)
        {
            var httpResponseMessage = await httpResponseMessageTask;

            if (httpResponseMessage.IsSuccessStatusCode)
                return httpResponseMessage;

            throw new JsonRestException(httpResponseMessage.StatusCode, httpResponseMessage.Content);
        }

    }
}
