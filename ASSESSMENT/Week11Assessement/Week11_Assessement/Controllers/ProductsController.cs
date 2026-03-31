using Microsoft.AspNetCore.Mvc;
using Week11_Assessement.Models;
using Week11_Assessement.Services;

namespace Week11_Assessement.Controllers;

public class ProductsController : Controller
{
    private static readonly List<string> SupportedPromoCodes = ["WINTER25", "FREESHIP"];

    private readonly IPricingService _pricingService;
    private readonly IProductCatalogService _productCatalogService;

    public ProductsController(
        IPricingService pricingService,
        IProductCatalogService productCatalogService)
    {
        _pricingService = pricingService;
        _productCatalogService = productCatalogService;
    }

    public IActionResult Index(string promoCode = "WINTER25")
    {
        ViewData["StatusMessage"] = TempData["StatusMessage"] as string;
        return View(BuildProductsLandingViewModel(promoCode));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddProduct([Bind(Prefix = "AddProduct")] AddProductInputModel addProduct, string promoCode = "WINTER25")
    {
        promoCode = NormalizePromoCode(promoCode);

        if (!ModelState.IsValid)
        {
            return View("Index", BuildProductsLandingViewModel(promoCode, addProduct));
        }

        _productCatalogService.AddProduct(new ProductItem
        {
            Id = Guid.NewGuid(),
            Name = addProduct.Name,
            BasePrice = addProduct.BasePrice
        });

        TempData["StatusMessage"] = $"Product '{addProduct.Name}' added.";

        return RedirectToAction(nameof(Index), new { promoCode });
    }

    public IActionResult Edit(Guid id, string promoCode = "WINTER25")
    {
        promoCode = NormalizePromoCode(promoCode);
        var product = _productCatalogService.GetProductById(id);
        if (product is null)
        {
            return NotFound();
        }

        ViewData["PromoCode"] = promoCode;

        return View(new EditProductInputModel
        {
            Id = product.Id,
            Name = product.Name,
            BasePrice = product.BasePrice
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditProductInputModel editProduct, string promoCode = "WINTER25")
    {
        promoCode = NormalizePromoCode(promoCode);

        if (!ModelState.IsValid)
        {
            ViewData["PromoCode"] = promoCode;
            return View(editProduct);
        }

        var updated = _productCatalogService.UpdateProduct(new ProductItem
        {
            Id = editProduct.Id,
            Name = editProduct.Name,
            BasePrice = editProduct.BasePrice
        });

        if (!updated)
        {
            return NotFound();
        }

        TempData["StatusMessage"] = $"Product '{editProduct.Name}' updated.";

        return RedirectToAction(nameof(Index), new { promoCode });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id, string promoCode = "WINTER25")
    {
        promoCode = NormalizePromoCode(promoCode);
        _productCatalogService.DeleteProduct(id);
        TempData["StatusMessage"] = "Product deleted.";
        return RedirectToAction(nameof(Index), new { promoCode });
    }

    private ProductsLandingViewModel BuildProductsLandingViewModel(string promoCode, AddProductInputModel? addProduct = null)
    {
        var normalizedPromo = NormalizePromoCode(promoCode);
        var products = _productCatalogService
            .GetProducts()
            .Select(p =>
            {
                var breakdown = _pricingService.CalculatePriceBreakdown(p.BasePrice, normalizedPromo);
                return new ProductPriceViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    BasePrice = p.BasePrice,
                    FinalPrice = breakdown.FinalPrice
                };
            })
            .OrderBy(p => p.Name)
            .ToList();

        var totalBasePrice = products.Sum(p => p.BasePrice);
        var totalDiscountedPrice = products.Sum(p => p.FinalPrice);
        var totalsBreakdown = _pricingService.CalculatePriceBreakdown(totalBasePrice, normalizedPromo);

        return new ProductsLandingViewModel
        {
            PromoCode = normalizedPromo,
            PromoMessage = totalsBreakdown.PromoMessage,
            SupportedPromoCodes = SupportedPromoCodes,
            Products = products,
            AddProduct = addProduct ?? new AddProductInputModel(),
            TotalBasePrice = totalBasePrice,
            TotalDiscountedPrice = totalDiscountedPrice,
            TotalSavings = Math.Max(0m, totalBasePrice - totalDiscountedPrice)
        };
    }

    private static string NormalizePromoCode(string? promoCode)
    {
        var normalized = promoCode?.Trim().ToUpperInvariant();
        return string.IsNullOrWhiteSpace(normalized) ? "WINTER25" : normalized;
    }
}
