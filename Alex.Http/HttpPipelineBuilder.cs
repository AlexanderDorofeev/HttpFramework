using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Logging;

namespace Alex.Http
{
    public class HttpPipelineBuilder : IHttpPipelineBuilder
    {
        private readonly HttpMessageHandler _primaryHandler;
        private readonly IList<DelegatingHandler> _additionHandlers = new List<DelegatingHandler>();

        public HttpPipelineBuilder(HttpMessageHandler primaryHandler)
        {
            _primaryHandler = primaryHandler ?? throw new ArgumentNullException(nameof(primaryHandler));
        }
        
        public IHttpPipelineBuilder UseLogging(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            Use(new LoggingScopeHttpMessageHandler(logger));
            return this;
        }

        public IHttpPipelineBuilder Use(DelegatingHandler handler)
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));
            if (handler.InnerHandler != null) throw new InvalidOperationException("Handler InnerHandler should not be set, it is pipeline responsibility");
            _additionHandlers.Add(handler);
            return this;
        }


        public HttpMessageHandler Build()
        {
            var next = _primaryHandler;
            for (var i = _additionHandlers.Count - 1; i >= 0; i--)
            {
                var handler = _additionHandlers[i];
                handler.InnerHandler = next;
                next = handler;
            }

            return next;
        }
    }
}