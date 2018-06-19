using System;
using System.Net;

namespace Binateq.JsonRestClient
{
    public class JsonRestException : Exception
    {
        public Uri Uri { get; }

        public HttpStatusCode StatusCode { get; }

        public string Content { get; }

        public JsonRestException(Uri uri, HttpStatusCode statusCode, string content)
            : base("Invalid HTTP status.")
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
