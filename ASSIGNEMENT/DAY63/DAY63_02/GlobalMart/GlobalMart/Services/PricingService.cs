namespace GlobalMart.Services
{
    // In this file we are decoupling the pricing logic from the controllers,
    // so that we can easily modify it in the future without having to change the controllers.
    public class PricingService : IPricingService
    {
        public decimal CalculateTotalPrice(decimal basePrice, string promoCode)
        {
            decimal finalPrice = basePrice;

            if(promoCode == "WINTER25")
            {
                finalPrice = basePrice * 0.85m; // 15% discount
            }
            else if (promoCode == "FREESHIP")
            {
                finalPrice = basePrice - 5;
            }

            return finalPrice;
        }

    }
}
