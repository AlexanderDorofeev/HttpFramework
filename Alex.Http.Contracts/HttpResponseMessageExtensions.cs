using System.Net.Http;
using System.Threading.Tasks;

namespace Alex.Http
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var body = string.Empty;
                if (response.Content != null)
                {
                    try
                    {
                        body = await response.Content.ReadAsStringAsync();
                    }
                    catch
                    {
                        //error during reading failed response
                    }
                }

                if (response.Headers.RetryAfter != null)
                {
                    throw new TransientResponseException(response.StatusCode, response.ReasonPhrase, body, response.Headers?.RetryAfter);
                }

                throw new ResponseException(response.StatusCode, response.ReasonPhrase, body);
            }
        }
    }
}