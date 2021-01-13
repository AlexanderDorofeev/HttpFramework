using System;
using System.Net;
using System.Net.Http.Headers;

namespace Alex.Http
{
    public class TransientResponseException : ResponseException
    {
        public TransientResponseException(HttpStatusCode statusCode, string reasonPhrase, string content, RetryConditionHeaderValue retryHeaderValue)
            : base(statusCode, reasonPhrase, content)
        {
            RetryHeaderValue = retryHeaderValue ?? throw new ArgumentNullException(nameof(retryHeaderValue));
        }

        public RetryConditionHeaderValue RetryHeaderValue { get; }
    }
}