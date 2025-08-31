using System.Text;
using System.Text.Json;
using static Jtbd.Webapp.Services.RepositoryGeneric;

namespace Jtbd.Webapp.Services
{
    public class RepositoryGeneric : IRepositoryGeneric
    {
        private readonly HttpClient _httpClient;

        private JsonSerializerOptions _jsonDefaultOptions => new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public RepositoryGeneric(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<object> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _jsonDefaultOptions)!;
        }

        public async Task<T> GetByIdAsync<T>(string url, int id)
        {
            var requestUrl = $"{url}/{id}";
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _jsonDefaultOptions)!;
        }

        public async Task<object> PostAsync<T>(string url, T model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<object> PutAsync<T>(string url, T model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}