using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Alex.Http
{
    public static class RequestHandlerJsonExtensions
    {
           public static Task<T> GetAsync<T>(this IHttpRequestHandler handler, string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExecuteRequestAsync<T>(handler, url, HttpMethod.Get, null, cancellationToken);
        }

        public static Task<T> PostAsync<T>(this IHttpRequestHandler handler, string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExecuteRequestAsync<T>(handler, url, HttpMethod.Post, null, cancellationToken);
        }

        public static Task<T> PostAsync<T,U>(this IHttpRequestHandler handler, string url, U request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return handler.ExecuteRequestAsync<T>(url, HttpMethod.Post, new JsonObjectContent(request), cancellationToken);
        }

        public static Task PostAsync<T>(this IHttpRequestHandler handler, string url, T request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return handler.ExecuteRequestAsync(url, HttpMethod.Post, new JsonObjectContent(request), cancellationToken);
        }

        public static Task PutAsync<T>(this IHttpRequestHandler handler, string url, T request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return handler.ExecuteRequestAsync(url, HttpMethod.Put, new JsonObjectContent(request), cancellationToken);
        }

        public static Task DeleteAsync(this IHttpRequestHandler handler, string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            return handler.ExecuteRequestAsync( url, HttpMethod.Delete, null, cancellationToken);
        }
        
        public static Task PatchAsync<T>(this IHttpRequestHandler handler, string url, T request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return handler.ExecuteRequestAsync(url, HttpMethod.Patch, new JsonObjectContent(request), cancellationToken);
        }
        
        
        public static async Task<T> ExecuteRequestAsync<T>(this IHttpRequestHandler handler, string url, HttpMethod method, HttpContent content = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await handler.ExecuteRequestAsync(url, method, content, cancellationToken);
            if (typeof(T) == typeof(string))
            {
                object body = string.Empty;
                if (response.Content != null)
                {
                    body = await response.Content.ReadAsStringAsync();
                }

                return (T) body;
            }

            if (typeof(T) == typeof(Stream))
            {
                object stream;
                if (response.Content != null)
                    stream = await response.Content.ReadAsStreamAsync();
                else
                    stream = new MemoryStream();
                return (T) stream;
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}