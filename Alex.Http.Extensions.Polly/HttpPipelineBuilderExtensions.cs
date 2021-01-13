using System.Net.Http;
using Microsoft.Extensions.Http;
using Polly;

namespace Alex.Http
{
    public static class HttpPipelinePollyExtensions
    {
        public static IHttpPipelineBuilder UsePolly(this IHttpPipelineBuilder builder, IAsyncPolicy<HttpResponseMessage> policy)
        {
            builder.Use(new PolicyHttpMessageHandler(policy));
            return builder;
        }
    }
}