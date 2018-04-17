using System;
using System.Collections.Generic;
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

        private JsonRestClient CreateJsonRestClientWithShortArraySerialization()
        {
            return new JsonRestClient(new HttpClient(), new Uri("http://api.domain.tld"),
                new JsonRestClientSettings { IsShortArraySerialization = true });
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

        [TestMethod]
        public void BuildUri_WithFormattableString_ReturnsSameUri()
        {
            var jsonRestClient = CreateJsonRestClient();
            var resourdeId = 1;
            var actual = jsonRestClient.BuildUri($"v1/resources/{resourdeId}");

            Assert.AreEqual(new Uri("http://api.domain.tld/v1/resources/1"), actual);
        }

        [TestMethod]
        public void BuildQueryString_WithEmptyInitialQueryString_ReturnsOnlyParameters()
        {
            var jsonRestClient = CreateJsonRestClient();
            var actual = jsonRestClient.BuildQueryString("", new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
            });

            Assert.AreEqual("x=1&y=2&z=3", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithEmptyParameters_ReturnsOnlyInitialQueryString()
        {
            var jsonRestClient = CreateJsonRestClient();
            var actual = jsonRestClient.BuildQueryString("a=4&b=5&c=6", new Dictionary<string, object>());

            Assert.AreEqual("a=4&b=5&c=6", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithNullParameter_IgnoresTheParameter()
        {
            var jsonRestClient = CreateJsonRestClient();
            var actual = jsonRestClient.BuildQueryString("", new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", null },
                { "z", 3 },
            });

            Assert.AreEqual("x=1&z=3", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithInitialQueryStringAndParameters_ReturnsBoth()
        {
            var jsonRestClient = CreateJsonRestClient();
            var actual = jsonRestClient.BuildQueryString("a=4&b=5&c=6", new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
            });

            Assert.AreEqual("a=4&b=5&c=6&x=1&y=2&z=3", actual);
        }

        [TestMethod]
        public void BuildClassicQueryString_WithArray_ReturnsLineariesArray()
        {
            var jsonRestClient = CreateJsonRestClient();
            var actual = jsonRestClient.BuildQueryString("", new Dictionary<string, object>
            {
                { "x", new [] {1, 2, 3 } },
            });

            Assert.AreEqual("x=1&x=2&x=3", actual);
        }

        [TestMethod]
        public void BuildShortQueryString_WithArray_ReturnsLineariesArray()
        {
            var jsonRestClient = CreateJsonRestClientWithShortArraySerialization();
            var actual = jsonRestClient.BuildQueryString("", new Dictionary<string, object>
            {
                { "x", new [] {1, 2, 3 } },
            });

            Assert.AreEqual("x=1%2c2%2c3", actual);
        }
    }
}
