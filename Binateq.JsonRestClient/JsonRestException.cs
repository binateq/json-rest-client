using System;
using System.Net;

namespace Binateq.JsonRestClient
{
    public class JsonRestException : Exception
    {
        public Uri Uri { get; }

        public string RequestContent { get; }

        public string ResponseContent { get; }

        public HttpStatusCode StatusCode { get; }

        [Obsolete]
        public string Content => RequestContent;

        public JsonRestException(Uri uri, string requestContent, string responseContent, HttpStatusCode statusCode)
            : base("Invalid HTTP status.")
        {
            Uri = uri;
            RequestContent = requestContent;
            ResponseContent = responseContent;
            StatusCode = statusCode;
        }
    }
}
