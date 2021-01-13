using System.Net.Http;

namespace Alex.Http
{
    public interface IHttpPipelineBuilder
    {
        IHttpPipelineBuilder Use(DelegatingHandler handler);
        HttpMessageHandler Build();
    }
}