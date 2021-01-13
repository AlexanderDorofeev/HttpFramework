using System;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.Timeout;

namespace Alex.Http
{
    public static class DefaultPolicyFactory
    {
        public  static IAsyncPolicy<HttpResponseMessage> TransientFaultRetry(int retryCount = 5, Func<int, TimeSpan> retrySleepDuration = null)
        {
            var sleepDuration = retrySleepDuration ?? (retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            return Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                .Or<TimeoutRejectedException>()
                .OrResult(response => response.StatusCode >= HttpStatusCode.InternalServerError || response.StatusCode == HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(retryCount, sleepDuration);
        }
    }
}