using Week11_Assessement.Models;

namespace Week11_Assessement.Services;

public class ProductCatalogService : IProductCatalogService
{
    private readonly object _gate = new();
    private readonly List<ProductItem> _products = new();

    public ProductCatalogService()
    {
        _products.AddRange(
        [
            new ProductItem { Id = Guid.NewGuid(), Name = "Nebula Laptop Pro", BasePrice = 1299.00m },
            new ProductItem { Id = Guid.NewGuid(), Name = "Aero Noise-Cancel Headphones", BasePrice = 219.00m },
            new ProductItem { Id = Guid.NewGuid(), Name = "TrailSmart Backpack", BasePrice = 89.00m }
        ]);
    }

    public IReadOnlyList<ProductItem> GetProducts()
    {
        lock (_gate)
        {
            return _products
                .Select(p => new ProductItem { Id = p.Id, Name = p.Name, BasePrice = p.BasePrice })
                .ToList();
        }
    }

    public ProductItem? GetProductById(Guid id)
    {
        lock (_gate)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product is null
                ? null
                : new ProductItem { Id = product.Id, Name = product.Name, BasePrice = product.BasePrice };
        }
    }

    public void AddProduct(ProductItem product)
    {
        lock (_gate)
        {
            if (_products.Any(p => string.Equals(p.Name, product.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            _products.Add(new ProductItem
            {
                Id = product.Id == Guid.Empty ? Guid.NewGuid() : product.Id,
                Name = product.Name.Trim(),
                BasePrice = product.BasePrice
            });
        }
    }

    public bool UpdateProduct(ProductItem product)
    {
        lock (_gate)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index < 0)
            {
                return false;
            }

            _products[index] = new ProductItem
            {
                Id = product.Id,
                Name = product.Name.Trim(),
                BasePrice = product.BasePrice
            };

            return true;
        }
    }

    public bool DeleteProduct(Guid id)
    {
        lock (_gate)
        {
            var removed = _products.RemoveAll(p => p.Id == id);
            return removed > 0;
        }
    }
}
