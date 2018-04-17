﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Binateq.JsonRestExtensions.Tests
{
    [TestClass]
    public class UriFormatProviderTests
    {
        [TestMethod]
        public void Format_WithNullArg_ReturnsEmptyString()
        {
            FormattableString formattableString = $"{null}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void Format_WithUriSafeArg_ReturnsSameArg()
        {
            var value = "URI-safe-string";
            FormattableString formattableString = $"{value}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("URI-safe-string", actual);
        }

        [TestMethod]
        public void Format_WithUriUnsafeArg_ReturnsUriEncodedArg()
        {
            var value = "URI unsafe string";
            FormattableString formattableString = $"{value}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("URI%20unsafe%20string", actual);
        }

        [TestMethod]
        public void Format_WithRawUnsafeArg_ReturnsSameArg()
        {
            var value = "URI unsafe string";
            FormattableString formattableString = $"{value:raw}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("URI unsafe string", actual);
        }

        [TestMethod]
        public void Format_WithDateTimeOffset_UsesBigOFormat()
        {
            var value = new DateTimeOffset(2018, 4, 16, 15, 30, 0, TimeSpan.FromHours(3));
            FormattableString formattableString = $"{value}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("2018-04-16T15:30:00.0000000+03:00", actual);
        }

        [TestMethod]
        public void Format_WithDateTime_UsesLittleSFormat()
        {
            var value = new DateTime(2018, 4, 16, 15, 30, 0);
            FormattableString formattableString = $"{value}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("2018-04-16T15:30:00", actual);
        }

        [TestMethod]
        public void Format_WithTimeSpan_UsesLittleCFormat()
        {
            var value = new TimeSpan(15, 30, 0);
            FormattableString formattableString = $"{value}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("15:30:00", actual);
        }

        [TestMethod]
        public void Format_WithFormattable_UsesSpecifiedFormat()
        {
            var value = Math.PI;
            FormattableString formattableString = $"{value:0.##}";

            var actual = formattableString.ToString(new UriFormatProvider());

            Assert.AreEqual("3.14", actual);
        }
    }
}
