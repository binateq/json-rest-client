using System;
using System.Globalization;

namespace Binateq.JsonRestExtensions
{
    /// <summary>
    /// Formats Uri strings.
    /// </summary>
    /// <remarks>
    /// <see cref="!:http://www.thomaslevesque.com/2015/02/24/customizing-string-interpolation-in-c-6/">Customizing string interpolation</see>.
    /// </remarks>
    internal class UriFormatProvider : IFormatProvider, ICustomFormatter
    {
        object IFormatProvider.GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;

            return null;
        }

        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                return string.Empty;

            if (format == "raw")
                return arg.ToString();

            return FormatPrimitive(format, arg);
        }

        internal static string FormatPrimitive(string format, object arg)
        {
            if (arg is DateTimeOffset dateTimeOffset && string.IsNullOrEmpty(format))
                return dateTimeOffset.ToString("O", CultureInfo.InvariantCulture);

            if (arg is DateTime dateTime && string.IsNullOrEmpty(format))
                return dateTime.ToString("s", CultureInfo.InvariantCulture);

            if (arg is TimeSpan timeSpan && string.IsNullOrEmpty(format))
                return timeSpan.ToString("c", CultureInfo.InvariantCulture);

            if (arg is IFormattable formattable)
                return formattable.ToString(format, CultureInfo.InvariantCulture);

            return Uri.EscapeDataString(arg.ToString());
        }
    }
}
