using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Binateq.JsonRestClient
{
    public static class HttpClientExtensions
    {
        private static HttpContent CreateJsonContent(object dto)
        {
            var json = JsonRestClientSettings.Default.Serialize(dto);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
		public static async Task<T> GetAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.GetAsync(requestUri
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.GetAsync(requestUri
			).ReadOrDefaultOrThrowAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.PutAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PutAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PutAsync(requestUri
				, null
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.PostAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PostAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PostAsync(requestUri
				, null
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.DeleteAsync(requestUri
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.DeleteAsync(requestUri
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.DeleteAsync(requestUri
			).ForgetAsync();
		}
		public static async Task<T> GetAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.GetAsync(requestUri
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.GetAsync(requestUri
			).ReadOrDefaultOrThrowAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.PutAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PutAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PutAsync(requestUri
				, null
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.PostAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PostAsync(requestUri
				, null
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PostAsync(requestUri
				, null
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.DeleteAsync(requestUri
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.DeleteAsync(requestUri
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.DeleteAsync(requestUri
			).ForgetAsync();
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PutAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PostAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			return await httpClient.SendAsync(message
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
			).ForgetAsync();
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PutAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PostAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			return await httpClient.SendAsync(message
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
			).ForgetAsync();
		}
		public static async Task<T> GetAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.GetAsync(requestUri
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.GetAsync(requestUri
				, cancellationToken
			).ReadOrDefaultOrThrowAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.PutAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PutAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PutAsync(requestUri
				, null
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.PostAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PostAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.PostAsync(requestUri
				, null
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			return await httpClient.DeleteAsync(requestUri
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.DeleteAsync(requestUri
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			await httpClient.DeleteAsync(requestUri
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> GetAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.GetAsync(requestUri
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.GetAsync(requestUri
				, cancellationToken
			).ReadOrDefaultOrThrowAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.PutAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PutAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PutAsync(requestUri
				, null
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.PostAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PostAsync(requestUri
				, null
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.PostAsync(requestUri
				, null
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			return await httpClient.DeleteAsync(requestUri
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.DeleteAsync(requestUri
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			await httpClient.DeleteAsync(requestUri
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PutAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PostAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			return await httpClient.SendAsync(message
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> PutAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PutAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PutAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PutAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PutAsync(requestUri
				, content
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> PostAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			return await httpClient.PostAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task PostAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task PostAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var content = CreateJsonContent(contentParameter);
			await httpClient.PostAsync(requestUri
				, content
				, cancellationToken
			).ForgetAsync();
		}
		public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			return await httpClient.SendAsync(message
				, cancellationToken
			).ThrowIfInvalidStatusAsync()
			 .ReadAsync<T>(JsonRestClientSettings.Default.Deserialize);
		}
		public static async Task DeleteAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
				, cancellationToken
			).ThrowIfInvalidStatusAsync();
		}
		public static async Task DeleteAndForgetAsync(this HttpClient httpClient, Uri baseUri, FormattableString formattableString
			, IReadOnlyDictionary<string, object> queryStringParameters
			, object contentParameter
			, CancellationToken cancellationToken
		)
		{
		    var requestUri = baseUri.Append(formattableString, JsonRestClientSettings.Default.IsShortArraySerialization, queryStringParameters);
			var message = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = CreateJsonContent(contentParameter),
			};
			await httpClient.SendAsync(message
				, cancellationToken
			).ForgetAsync();
		}
	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task DeleteAsync(this HttpClient httpClient, FormattableString formattableString) =>
		await DeleteAsync(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task DeleteAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await DeleteAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task DeleteAsync(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await DeleteAsync(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task DeleteAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await DeleteAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task DeleteAndForgetAsync(this HttpClient httpClient, FormattableString formattableString) =>
		await DeleteAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task DeleteAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await DeleteAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task DeleteAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await DeleteAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task DeleteAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await DeleteAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task<T> GetAsync<T>(this HttpClient httpClient, FormattableString formattableString) =>
		await GetAsync<T>(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task<T> GetAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await GetAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task<T> GetAsync<T>(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await GetAsync<T>(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task<T> GetAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await GetAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, FormattableString formattableString) =>
		await GetOrDefaultAsync<T>(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await GetOrDefaultAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await GetOrDefaultAsync<T>(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task<T> GetOrDefaultAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await GetOrDefaultAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, FormattableString formattableString) =>
		await DeleteAsync<T>(httpClient, httpClient.BaseAddress, formattableString);

	public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters) =>
		await DeleteAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters);

	public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, FormattableString formattableString, CancellationToken cancellationToken) =>
		await DeleteAsync<T>(httpClient, httpClient.BaseAddress, formattableString, cancellationToken);

	public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, CancellationToken cancellationToken) =>
		await DeleteAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, cancellationToken);
	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter);

	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter);

	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter, CancellationToken cancellationToken) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter, cancellationToken);

	public static async Task PutAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter, CancellationToken cancellationToken) =>
		await PutAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter, cancellationToken);
	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter);

	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter);

	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter, CancellationToken cancellationToken) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter, cancellationToken);

	public static async Task PutAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter, CancellationToken cancellationToken) =>
		await PutAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter, cancellationToken);
	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter);

	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter);

	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter, CancellationToken cancellationToken) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter, cancellationToken);

	public static async Task PostAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter, CancellationToken cancellationToken) =>
		await PostAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter, cancellationToken);
	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter);

	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter);

	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, object contentParameter, CancellationToken cancellationToken) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, contentParameter, cancellationToken);

	public static async Task PostAndForgetAsync(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter, CancellationToken cancellationToken) =>
		await PostAndForgetAsync(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter, cancellationToken);
	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, object contentParameter) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, contentParameter);

	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter);

	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, object contentParameter, CancellationToken cancellationToken) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, contentParameter, cancellationToken);

	public static async Task<T> PutAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter, CancellationToken cancellationToken) =>
		await PutAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter, cancellationToken);
	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, object contentParameter) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, contentParameter);

	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter);

	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, object contentParameter, CancellationToken cancellationToken) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, contentParameter, cancellationToken);

	public static async Task<T> PostAsync<T>(this HttpClient httpClient, FormattableString formattableString, IReadOnlyDictionary<string, object> queryStringParameters, object contentParameter, CancellationToken cancellationToken) =>
		await PostAsync<T>(httpClient, httpClient.BaseAddress, formattableString, queryStringParameters, contentParameter, cancellationToken);
	}
}
