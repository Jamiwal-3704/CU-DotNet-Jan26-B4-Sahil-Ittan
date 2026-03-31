using Week11_Assessement.Models;

namespace Week11_Assessement.Services;

public interface IPricingService
{
    decimal CalculateFinalPrice(decimal basePrice, string? promoCode);
    PricingBreakdown CalculatePriceBreakdown(decimal basePrice, string? promoCode);
}
