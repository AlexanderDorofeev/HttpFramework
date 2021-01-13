using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Alex.Http
{
    public class JsonObjectContent : StringContent
    {
        public JsonObjectContent(object content)
            : base(SerializeToJson(content), Encoding.UTF8, "application/json")
        {
        }

        public JsonObjectContent(object content, JsonSerializerSettings serializerSettings)
            : base(SerializeToJson(content, serializerSettings), Encoding.UTF8, "application/json")
        {
        }

        private static string SerializeToJson(object content, JsonSerializerSettings serializerSettings = null)
        {
            if (content == null) return string.Empty;

            return serializerSettings != null ? JsonConvert.SerializeObject(content, serializerSettings) : JsonConvert.SerializeObject(content);
        }
    }
}