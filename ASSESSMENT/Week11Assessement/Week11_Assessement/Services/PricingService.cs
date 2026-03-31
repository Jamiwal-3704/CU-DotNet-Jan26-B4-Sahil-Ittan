using Week11_Assessement.Models;

namespace Week11_Assessement.Services;

public class PricingService : IPricingService
{
    public decimal CalculateFinalPrice(decimal basePrice, string? promoCode)
    {
        return CalculatePriceBreakdown(basePrice, promoCode).FinalPrice;
    }

    public PricingBreakdown CalculatePriceBreakdown(decimal basePrice, string? promoCode)
    {
        var normalizedCode = promoCode?.Trim().ToUpperInvariant();
        var finalPrice = normalizedCode switch
        {
            "WINTER25" => Math.Round(basePrice * 0.85m, 2),
            "FREESHIP" => Math.Max(0m, basePrice - 5.00m),
            _ => basePrice
        };

        var appliedPromo = normalizedCode is "WINTER25" or "FREESHIP" ? normalizedCode : "NONE";
        var promoMessage = appliedPromo switch
        {
            "WINTER25" => "Winter Sale Applied: 15% OFF",
            "FREESHIP" => "Shipping Boost Applied: $5 OFF",
            _ => "No promo applied"
        };

        return new PricingBreakdown
        {
            AppliedPromoCode = appliedPromo,
            PromoMessage = promoMessage,
            OriginalPrice = basePrice,
            FinalPrice = finalPrice,
            Savings = Math.Max(0m, Math.Round(basePrice - finalPrice, 2))
        };
    }
}
