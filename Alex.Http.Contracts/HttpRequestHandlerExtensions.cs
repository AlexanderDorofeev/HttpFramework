using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Alex.Http
{
    public static class HttpRequestHandlerExtensions
    {
        public static async Task<HttpResponseMessage> ExecuteRequestAsync(this IHttpRequestHandler handler, string url, HttpMethod method, HttpContent content = null,
            CancellationToken cancellationToken = default)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (method == null) throw new ArgumentNullException(nameof(method));

            var request = new HttpRequestMessage(method, url) { Content = content };
            var response = await handler.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
            return response;
        }
    }
}