using Microsoft.EntityFrameworkCore;
using NorthwindCatalog.Services.DTOs;
using NorthwindCatalog.Services.Models;

namespace NorthwindCatalog.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        // 🔹 Get Products by Category
        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        // 🔹 Summary Logic (MOST IMPORTANT PART)
        public async Task<IEnumerable<CategorySummaryDto>> GetCategorySummariesAsync()
        {
            return await _context.Categories
                .Select(c => new CategorySummaryDto
                {
                    CategoryName = c.CategoryName,

                    ProductCount = c.Products.Count(),

                    // important fix
                    AvgPrice = c.Products.Select(p => (decimal?)p.UnitPrice).Average() ?? 0m,

                    MostExpensiveProduct = c.Products
                        .OrderByDescending(p => p.UnitPrice)
                        .Select(p => p.ProductName)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}
