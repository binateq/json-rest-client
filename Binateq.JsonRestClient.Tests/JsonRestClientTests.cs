using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binateq.JsonRestClient.Tests
{
    [TestClass]
    public class JsonRestClientTests
    {
        private JsonRestClient CreateJsonRestClient()
        {
            return new JsonRestClient(new HttpClient(), new Uri("http://api.domain.tld"));
        }

        [TestMethod]
        public async Task CreateJsonContent_WithNull_ReturnsNullJson()
        {
            var jsonRestClient = CreateJsonRestClient();
            var stringContent = jsonRestClient.CreateJsonContent(null);

            var actual = await stringContent.ReadAsStringAsync();

            Assert.AreEqual("null", actual);
        }

        [TestMethod]
        public async Task CreateJsonContent_WithObject_ReturnsObjectJson()
        {
            var jsonRestClient = CreateJsonRestClient();
            var stringContent = jsonRestClient.CreateJsonContent(new { Foo = "foo", Bar = 3.14 });

            var actual = await stringContent.ReadAsStringAsync();

            Assert.AreEqual("{\r\n  \"foo\": \"foo\",\r\n  \"bar\": 3.14\r\n}", actual);
        }

        [TestMethod]
        public async Task CreateJsonContent_WithArray_ReturnsArrayJson()
        {
            var jsonRestClient = CreateJsonRestClient();
            var stringContent = jsonRestClient.CreateJsonContent(new [] { 1, 1, 2, 3, 5, 8 });

            var actual = await stringContent.ReadAsStringAsync();

            Assert.AreEqual("[\r\n  1,\r\n  1,\r\n  2,\r\n  3,\r\n  5,\r\n  8\r\n]", actual);
        }
    }
}
