namespace Week11_Assessement.Models;

public class CartSummaryViewModel
{
    public required string PromoCode { get; set; }
    public required string PromoMessage { get; set; }
    public required string AppliedPromoCode { get; set; }
    public required List<CartLineItemViewModel> Items { get; set; }
    public int ItemCount { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Savings { get; set; }
    public decimal FinalTotal { get; set; }
}
