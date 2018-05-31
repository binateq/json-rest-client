using System;
using System.Globalization;
using System.Web;

namespace Binateq.JsonRestClient
{
    /// <inheritdoc cref="IFormatProvider" />
    /// <summary>
    /// Formats Uri strings.
    /// </summary>
    /// <remarks>
    /// <see cref="!:http://www.thomaslevesque.com/2015/02/24/customizing-string-interpolation-in-c-6/">Customizing string interpolation</see>.
    /// </remarks>
    internal class UriFormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <inheritdoc />
        object IFormatProvider.GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;

            return null;
        }

        /// <inheritdoc />
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                return string.Empty;

            if (format == "raw")
                return arg.ToString();

            return FormatAndEscape(format, arg);
        }

        /// <summary>
        /// Formats and escapes single value depending on its type.
        /// </summary>
        /// <remarks>
        /// Escapes formatted string if needed.
        /// </remarks>
        /// <param name="format">Format, can be <see cref="string.Empty"/>.</param>
        /// <param name="value">Value.</param>
        /// <returns>Formatted string representation of <paramref name="value"/>.</returns>
        public static string FormatAndEscape(string format, object value)
        {
            var formattedValue = Format(format, value);

            return HttpUtility.UrlEncode(formattedValue);
        }

        private static string Format(string format, object value)
        {
            if (value is DateTimeOffset dateTimeOffset && string.IsNullOrEmpty(format))
                return dateTimeOffset.ToString("O", CultureInfo.InvariantCulture);

            if (value is DateTime dateTime && string.IsNullOrEmpty(format))
                return dateTime.ToString("s", CultureInfo.InvariantCulture);

            if (value is TimeSpan timeSpan && string.IsNullOrEmpty(format))
                return timeSpan.ToString("c", CultureInfo.InvariantCulture);

            if (value is IFormattable formattable)
                return formattable.ToString(format, CultureInfo.InvariantCulture);

            return value.ToString();
        }
    }
}
