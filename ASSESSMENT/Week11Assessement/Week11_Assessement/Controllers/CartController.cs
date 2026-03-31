using Microsoft.AspNetCore.Mvc;
using Week11_Assessement.Models;
using Week11_Assessement.Services;

namespace Week11_Assessement.Controllers;

public class CartController : Controller
{
    private readonly IPricingService _pricingService;
    private readonly IProductCatalogService _productCatalogService;

    public CartController(
        IPricingService pricingService,
        IProductCatalogService productCatalogService)
    {
        _pricingService = pricingService;
        _productCatalogService = productCatalogService;
    }

    public IActionResult Index(string promoCode = "WINTER25")
    {
        var normalizedPromo = NormalizePromoCode(promoCode);
        var products = _productCatalogService.GetProducts();

        var items = products
            .Select(product =>
            {
                var breakdown = _pricingService.CalculatePriceBreakdown(product.BasePrice, normalizedPromo);
                return new CartLineItemViewModel
                {
                    Name = product.Name,
                    BasePrice = product.BasePrice,
                    DiscountedPrice = breakdown.FinalPrice
                };
            })
            .OrderBy(item => item.Name)
            .ToList();

        var subtotal = items.Sum(item => item.BasePrice);
        var totalsBreakdown = _pricingService.CalculatePriceBreakdown(subtotal, normalizedPromo);

        var viewModel = new CartSummaryViewModel
        {
            PromoCode = normalizedPromo,
            PromoMessage = totalsBreakdown.PromoMessage,
            AppliedPromoCode = totalsBreakdown.AppliedPromoCode,
            Items = items,
            ItemCount = items.Count,
            Subtotal = subtotal,
            Savings = totalsBreakdown.Savings,
            FinalTotal = totalsBreakdown.FinalPrice
        };

        return View(viewModel);
    }

    private static string NormalizePromoCode(string? promoCode)
    {
        var normalized = promoCode?.Trim().ToUpperInvariant();
        return string.IsNullOrWhiteSpace(normalized) ? "WINTER25" : normalized;
    }
}
