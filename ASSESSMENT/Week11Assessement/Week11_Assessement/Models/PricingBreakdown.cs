namespace Week11_Assessement.Models;

public class PricingBreakdown
{
    public required string AppliedPromoCode { get; set; }
    public required string PromoMessage { get; set; }
    public decimal OriginalPrice { get; set; }
    public decimal FinalPrice { get; set; }
    public decimal Savings { get; set; }
}
