using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Alex.Http
{
    public interface IHttpRequestHandler
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    }
}