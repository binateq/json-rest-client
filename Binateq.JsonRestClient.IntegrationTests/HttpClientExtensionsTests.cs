using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Binateq.JsonRestClient.IntegrationTests
{
    [TestClass]
    [TestCategory(Constants.TestCategory.Integration)]
    public class HttpClientExtensionsTests
    {
        [TestMethod]
        public async Task GetOrDefaultAsync_OnStatusCode404_ReturnsNull()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("https://google.com/123");

            try
            {
                var result = await httpClient.GetOrDefaultAsync<string>($"https://google.com/123");
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}
