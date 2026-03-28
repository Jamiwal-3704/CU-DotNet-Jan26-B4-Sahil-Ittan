
namespace GlobalMart.Services
{
    public interface IPricingService
    {
        decimal CalculateTotalPrice(decimal basePrice, string promoCode);
    }
}
