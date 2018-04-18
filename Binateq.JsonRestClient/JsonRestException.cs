using System;
using System.Net;

namespace Binateq.JsonRestClient
{
    public class JsonRestException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public string Content { get; }

        public JsonRestException(HttpStatusCode statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;
        }
    }
}
