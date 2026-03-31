namespace Week11_Assessement.Models;

public class ProductsLandingViewModel
{
    public required string PromoCode { get; set; }
    public required string PromoMessage { get; set; }
    public required List<string> SupportedPromoCodes { get; set; }
    public required List<ProductPriceViewModel> Products { get; set; }
    public AddProductInputModel AddProduct { get; set; } = new();
    public decimal TotalBasePrice { get; set; }
    public decimal TotalDiscountedPrice { get; set; }
    public decimal TotalSavings { get; set; }
}
