using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binateq.JsonRestClient.Tests
{
    [TestClass]
    public class HttpResponseMessageTaskExtensionsTests
    {
        class TestObject { public string A { get; set; } public int B { get; set; } };

        [TestMethod]
        public async Task ReadContentAsync_WhenCalled_RunsJsonDeserialize()
        {
            var httpResponseMessageTask = Task.FromResult(new HttpResponseMessage
            {
                Content = new StringContent(string.Empty),
            });

            object JsonDeserialize(string json, Type type) => new TestObject {A = "foo", B = 100};

            var actual = await httpResponseMessageTask.ReadAsync<TestObject>(JsonDeserialize);

            Assert.AreEqual("foo", actual.A);
            Assert.AreEqual(100, actual.B);
        }

        [TestMethod]
        public async Task ThrowIfInvalidStatusAsync_WithStatusOk_ReturnsResponse()
        {
            var httpResponseMessageTask = Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });

            var actual = await httpResponseMessageTask.ThrowIfInvalidStatusAsync();

            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonRestException))]
        public async Task ThrowIfInvalidStatusAsync_WithStatusBadRequest_ThrowsException()
        {
            var httpResponseMessageTask = Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });

            var actual = await httpResponseMessageTask.ThrowIfInvalidStatusAsync();
        }
    }
}
