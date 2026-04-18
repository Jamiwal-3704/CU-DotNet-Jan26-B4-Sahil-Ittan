using NorthwindCatalog.Web.DTOs;

namespace NorthwindCatalog.Web.DTOs
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; } = Enumerable.Empty<CategoryDto>();
        public IEnumerable<ProductDto> Products { get; set; } = Enumerable.Empty<ProductDto>();
        public IEnumerable<CategorySummaryDto> Summaries { get; set; } = Enumerable.Empty<CategorySummaryDto>();
        public int? SelectedCategoryId { get; set; }
        public string ActiveTab { get; set; } = "catalog";
    }
}
