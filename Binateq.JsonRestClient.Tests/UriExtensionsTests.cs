using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Binateq.JsonRestClient.Tests
{
    [TestClass]
    public class UriExtensionsTests
    {

        [TestMethod]
        public void Append_WithTwoQueries_CombinesQueries()
        {
            var baseUri = new Uri("http://localhost:11/path1/path2/?param1=1&param2=s");
            var id = 100;
            var actual = baseUri.Append($"param0/{id}", false, new Dictionary<string, object>
            {
                {"param3", true},
            }).ToString();

            Assert.AreEqual("http://localhost:11/path1/path2/param0/100?param1=1&param2=s&param3=True", actual);
        }

        [TestMethod]
        public void Append_WithFormattableString_ReturnsSameUri()
        {
            var baseUri = new Uri("http://api.domain.tld");
            var resourceId = 1;
            var actual = baseUri.Append($"v1/resources/{resourceId}");

            Assert.AreEqual(new Uri("http://api.domain.tld/v1/resources/1"), actual);
        }

        [TestMethod]
        public void BuildQueryString_WithEmptyInitialQueryString_ReturnsOnlyParameters()
        {
            var actual = UriExtensions.BuildQueryString("", false, new Dictionary<string, object>
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
            var actual = UriExtensions.BuildQueryString("a=4&b=5&c=6", false, new Dictionary<string, object>());

            Assert.AreEqual("a=4&b=5&c=6", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithNullParameter_IgnoresTheParameter()
        {
            var actual = UriExtensions.BuildQueryString("", false, new Dictionary<string, object>
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
            var actual = UriExtensions.BuildQueryString("a=4&b=5&c=6", false, new Dictionary<string, object>
            {
                { "x", 1 },
                { "y", 2 },
                { "z", 3 },
            });

            Assert.AreEqual("a=4&b=5&c=6&x=1&y=2&z=3", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithoutShortArraySerialization_ReturnsLongQueryString()
        {
            var actual = UriExtensions.BuildQueryString("", false, new Dictionary<string, object>
            {
                { "x", new [] {1, 2, 3 } },
            });

            Assert.AreEqual("x=1&x=2&x=3", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithShortArraySerialization_ReturnsShortQueryString()
        {
            var actual = UriExtensions.BuildQueryString("", true, new Dictionary<string, object>
            {
                { "x", new [] {1, 2, 3 } },
            });

            Assert.AreEqual("x=1%2c2%2c3", actual);
        }

        [TestMethod]
        public void BuildQueryString_WithStringParameter_ReturnsFullParameter()
        {
            var actual = UriExtensions.BuildQueryString("", false, new Dictionary<string, object>
            {
                { "x", "string" },
            });

            Assert.AreEqual("x=string", actual);
        }
    }
}
