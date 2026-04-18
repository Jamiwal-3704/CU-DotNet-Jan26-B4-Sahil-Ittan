using NorthwindCatalog.Web.DTOs;

namespace NorthwindCatalog.Web.Services
{
    public interface INorthwindApiClient
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<IEnumerable<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<CategorySummaryDto>> GetCategorySummaryAsync();
    }
}