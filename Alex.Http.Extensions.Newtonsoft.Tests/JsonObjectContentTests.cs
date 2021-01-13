using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Alex.Http.Tests
{
    public class JsonObjectContentTests
    {
        [Fact]
        public async Task JsonObjectContent_FromNullObject_ProducesEmptyBody()
        {
            var content = new JsonObjectContent(null);
            var body =await content.ReadAsStringAsync();
            Assert.Equal(string.Empty, body);
        }

        [Fact]
        public void JsonObjectContent_ContentTypeHeader_IsApplicationJson()
        {
            var content = new JsonObjectContent(new { });
            Assert.Equal("application/json", content.Headers.ContentType.MediaType);
            Assert.Equal("utf-8", content.Headers.ContentType.CharSet);
        }

        [Fact]
        public async Task JsonObjectContent_Honors_JsonSerializerSettings()
        {
            var content = new JsonObjectContent(new { SomeValue=(string)null },new JsonSerializerSettings { NullValueHandling=NullValueHandling.Ignore });
            var body = await content.ReadAsStringAsync();
            Assert.Equal("{}", body);
        }

        [Fact]
        public async Task JsonObjectContent_Produces_ValidObject()
        {
            var expected = new Fixture { Name = "John", Age = 35 };
            var content = new JsonObjectContent(expected);
            var body = await content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Fixture>(body);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Age, actual.Age);
        }

        private class Fixture
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }

    }
}