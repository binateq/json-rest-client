using System;
using System.Net;
using System.Net.Http;

namespace Binateq.JsonRestExtensions
{
    public class JsonRestException : Exception
    {
        HttpStatusCode StatusCode { get; }

        HttpContent Content { get; }

        public JsonRestException(HttpStatusCode statusCode, HttpContent content)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
