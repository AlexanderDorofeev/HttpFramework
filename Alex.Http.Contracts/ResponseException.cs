using System;
using System.Net;
using System.Net.Http;

namespace Alex.Http
{
    public class ResponseException : HttpRequestException
    {
        public ResponseException(HttpStatusCode httpStatusCode, string reasonPhrase, string content)
            : base($"Request failed with {httpStatusCode} status code ({reasonPhrase}).{Environment.NewLine+content}")
        {
            StatusCode = httpStatusCode;
            ReasonPhrase = reasonPhrase;
            Content = content;
        }

        public HttpStatusCode StatusCode { get; }

        public string Content { get; }

        public string ReasonPhrase { get; }
    }
}