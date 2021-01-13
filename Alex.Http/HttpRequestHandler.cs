using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alex.Http
{
    public class HttpRequestHandler : IHttpRequestHandler
    {
        private readonly HttpClient _httpClient;

        public HttpRequestHandler(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return _httpClient.SendAsync(request, cancellationToken);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return _httpClient.SendAsync(request, completionOption, cancellationToken);
        }
    }
}