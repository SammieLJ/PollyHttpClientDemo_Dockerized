using System.Net.Http;
using System.Threading.Tasks;

namespace PollyHttpClientDemo.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApiClient");
        }

        public async Task<string> GetDataAsync()
        {
            var response = await _httpClient.GetAsync("/data");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
