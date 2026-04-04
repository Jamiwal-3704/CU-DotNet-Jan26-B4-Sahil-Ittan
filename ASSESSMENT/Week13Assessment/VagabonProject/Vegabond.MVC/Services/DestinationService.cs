using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Vegabond.MVC.Models;

namespace Vegabond.MVC.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public DestinationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            var resp = await _httpClient.GetAsync("api/destinations");
            resp.EnsureSuccessStatusCode();
            var stream = await resp.Content.ReadAsStreamAsync();
            var items = await JsonSerializer.DeserializeAsync<IEnumerable<Destination>>(stream, _jsonOptions);
            return items ?? Enumerable.Empty<Destination>();
        }

        public async Task<Destination?> GetByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"api/destinations/{id}");
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Destination>(_jsonOptions);
        }

        public async Task AddAsync(Destination destination)
        {
            var resp = await _httpClient.PostAsJsonAsync("api/destinations", destination);
            resp.EnsureSuccessStatusCode();

            // If API returns the created entity with Id, update the local instance
            var created = await resp.Content.ReadFromJsonAsync<Destination>(_jsonOptions);
            if (created is not null)
            {
                destination.Id = created.Id;
            }
        }

        public async Task<Destination> CreateAsync(Destination destination)
        {
            var resp = await _httpClient.PostAsJsonAsync("api/destinations", destination);
            resp.EnsureSuccessStatusCode();
            var created = await resp.Content.ReadFromJsonAsync<Destination>(_jsonOptions);
            return created!;
        }

        public async Task UpdateAsync(Destination destination)
        {
            var resp = await _httpClient.PutAsJsonAsync($"api/destinations/{destination.Id}", destination);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _httpClient.DeleteAsync($"api/destinations/{id}");
            resp.EnsureSuccessStatusCode();
        }
    }
}