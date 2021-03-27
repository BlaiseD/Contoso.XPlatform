using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.Api
{
    public static class HttpClientExtenstions
    {
        #region Constants
        private const string WEB_REQUEST_CONTENT_TYPE = "application/json";
        private const string BASE_URL = "http://localhost:55688/api/";
        #endregion Constants

        public static async Task<TResult> PutAsync<TResult>(this IHttpClientFactory factory, string url, string jsonObject)
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result = await httpClient.PutAsync(GetUrl(url), GetStringContent(jsonObject));
            }

            result.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            );
        }

        public static async Task<TResult> PostAsync<TResult>(this IHttpClientFactory factory, string url, string jsonObject)
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result =  await httpClient.PostAsync(GetUrl(url), GetStringContent(jsonObject));
            }

            result.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            );
        }

        public static async Task<TResult> GetAsync<TResult>(this IHttpClientFactory factory, string url)
        {
            HttpResponseMessage result;
            using (HttpClient httpClient = factory.CreateClient())
            {
                result = await httpClient.GetAsync(GetUrl(url));
            }

            result.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<TResult>
            (
                await result.Content.ReadAsStringAsync(),
                SerializationOptions.Default
            );
        }

        private static StringContent GetStringContent(string jsonObject) 
            => new StringContent
            (
                jsonObject,
                Encoding.UTF8,
                WEB_REQUEST_CONTENT_TYPE
            );

        private static string GetUrl(string url) => $"{BASE_URL}{url}";
    }
}
