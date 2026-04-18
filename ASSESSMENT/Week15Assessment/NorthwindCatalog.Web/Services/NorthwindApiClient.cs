using System.Net.Http.Json;
using NorthwindCatalog.Web.DTOs;

namespace NorthwindCatalog.Web.Services
{
    public class NorthwindApiClient : INorthwindApiClient
    {
        private readonly IHttpClientFactory _factory;

        public NorthwindApiClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var client = _factory.CreateClient("NorthwindApi");
            var data = await client.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/categories");
            return data ?? Enumerable.Empty<CategoryDto>();
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var client = _factory.CreateClient("NorthwindApi");
            var data = await client.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/products/by-category/{categoryId}");
            return data ?? Enumerable.Empty<ProductDto>();
        }

        public async Task<IEnumerable<CategorySummaryDto>> GetCategorySummaryAsync()
        {
            var client = _factory.CreateClient("NorthwindApi");
            var data = await client.GetFromJsonAsync<IEnumerable<CategorySummaryDto>>("api/products/summary");
            return data ?? Enumerable.Empty<CategorySummaryDto>();
        }
    }
}