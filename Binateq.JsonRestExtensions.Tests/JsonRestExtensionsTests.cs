using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Binateq.JsonRestExtensions.Tests
{
    [TestClass]
    public class JsonRestExtensionsTests
    {
        [TestMethod]
        public async Task CreateJsonContent_WithNull_ReturnsNullJson()
        {
            var stringContent = JsonRestExtensions.CreateJsonContent(null);

            var actual = await stringContent.ReadAsStringAsync();

            Assert.AreEqual("null", actual);
        }

        [TestMethod]
        public async Task CreateJsonContent_WithObject_ReturnsObjectJson()
        {
            var stringContent = JsonRestExtensions.CreateJsonContent(new { Foo = "foo", Bar = 3.14 });

            var actual = await stringContent.ReadAsStringAsync();

            Assert.AreEqual("{\r\n  \"foo\": \"foo\",\r\n  \"bar\": 3.14\r\n}", actual);
        }

        [TestMethod]
        public async Task CreateJsonContent_WithArray_ReturnsArrayJson()
        {
            var stringContent = JsonRestExtensions.CreateJsonContent(new [] { 1, 1, 2, 3, 5, 8 });

            var actual = await stringContent.ReadAsStringAsync();

            Assert.AreEqual("[\r\n  1,\r\n  1,\r\n  2,\r\n  3,\r\n  5,\r\n  8\r\n]", actual);
        }

        [TestMethod]
        public void BuildUri_WithFormattableString_ReturnsSameUri()
        {
            var resourdeId = 1;
            var actual = JsonRestExtensions.BuildUri($"http://domain.tld/resources/{resourdeId}");

            Assert.AreEqual(new Uri("http://domain.tld/resources/1"), actual);
        }

        [TestMethod]
        public void BuildUri_WithBaseUri_ReturnsCombinedUri()
        {
            var baseUri = new Uri("http://domain.tld");
            var resourdeId = 1;
            var actual = JsonRestExtensions.BuildUri(baseUri, $"resources/{resourdeId}");

            Assert.AreEqual(new Uri("http://domain.tld/resources/1"), actual);
        }

        class TestObject { public string A { get; set; } public int B { get; set; } };

        [TestMethod]
        public async Task ReadContentAsync_WithValidJson_ReturnsObject()
        {
            var httpResponseMessageTask = Task.FromResult(new HttpResponseMessage
            {
                Content = new StringContent("{ a: 'foo', b: 100 }"),
            });

            var actual = await JsonRestExtensions.ReadContentAsync<TestObject>(httpResponseMessageTask);

            Assert.AreEqual("foo", actual.A);
            Assert.AreEqual(100, actual.B);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonReaderException))]
        public async Task ReadContentAsync_WithInvalidJson_ThrowsException()
        {
            var httpResponseMessageTask = Task.FromResult(new HttpResponseMessage
            {
                Content = new StringContent("{ a: 100, b: 'foo' }"),
            });

            var actual = await JsonRestExtensions.ReadContentAsync<TestObject>(httpResponseMessageTask);

            Assert.AreEqual("foo", actual.A);
        }

        [TestMethod]
        public async Task ThrowIfInvalidStatusAsync_WithOkResponse_ReturnsResponse()
        {
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
            var responseTask = Task.FromResult(response);

            var actual = await JsonRestExtensions.ThrowIfInvalidStatusAsync(responseTask);

            Assert.AreEqual(response, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonRestException))]
        public async Task ThrowIfInvalidStatusAsync_WithBadResponse_ThrowsException()
        {
            var response = new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
            var responseTask = Task.FromResult(response);

            var actual = await JsonRestExtensions.ThrowIfInvalidStatusAsync(responseTask);
        }

        [TestMethod]
        public void ToQueryString_WithEmptyInitialQueryString_ReturnsOnlyParameters()
        {
            var actual = JsonRestExtensions.ToQueryString("", new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
            });

            Assert.AreEqual("x=1&y=2&z=3", actual);
        }

        [TestMethod]
        public void ToQueryString_WithEmptyParameters_ReturnsOnlyInitialQueryString()
        {
            var actual = JsonRestExtensions.ToQueryString("a=4&b=5&c=6", new Dictionary<string, object>());

            Assert.AreEqual("a=4&b=5&c=6", actual);
        }

        [TestMethod]
        public void ToQueryString_WithNullParameter_IgnoresTheParameter()
        {
            var actual = JsonRestExtensions.ToQueryString("", new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", null },
                { "z", 3 },
            });

            Assert.AreEqual("x=1&z=3", actual);
        }

        [TestMethod]
        public void ToQueryString_WithInitialQueryStringAndParameters_ReturnsBoth()
        {
            var actual = JsonRestExtensions.ToQueryString("a=4&b=5&c=6", new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
            });

            Assert.AreEqual("a=4&b=5&c=6&x=1&y=2&z=3", actual);
        }

        [TestMethod]
        public void ToQueryString_WithArray_ReturnsLineariesArray()
        {
            var actual = JsonRestExtensions.ToQueryString("", new Dictionary<string, object>
            {
                { "x", new [] {1, 2, 3 } },
            });

            Assert.AreEqual("x=1%2c2%2c3", actual);
        }
    }
}
