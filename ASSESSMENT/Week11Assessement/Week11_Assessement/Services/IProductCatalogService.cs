using Week11_Assessement.Models;

namespace Week11_Assessement.Services;

public interface IProductCatalogService
{
    IReadOnlyList<ProductItem> GetProducts();
    ProductItem? GetProductById(Guid id);
    void AddProduct(ProductItem product);
    bool UpdateProduct(ProductItem product);
    bool DeleteProduct(Guid id);
}
