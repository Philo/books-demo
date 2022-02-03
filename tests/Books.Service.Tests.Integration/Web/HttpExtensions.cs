using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Books.Service.Tests.Integration.Web
{
    public static class HttpExtensions
    {
        public static async Task<HttpResponseMessage> PostObjectAsync(this HttpClient httpClient, string endpoint, object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(endpoint, httpContent);
        }

        public static async Task<HttpResponseMessage> PutObjectAsync(this HttpClient httpClient, string endpoint, object obj)
        {
            var json = JsonSerializer.Serialize(obj);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            return await httpClient.PutAsync(endpoint, httpContent);
        }

        public static async Task<T?> ReadAsObjectAsync<T>(this HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();
            if(stream == null)
            {
                return default;
            }

            return await JsonSerializer.DeserializeAsync<T>(stream, 
                new JsonSerializerOptions 
                { 
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNameCaseInsensitive = true 
                }
            );
        }
    }
}