namespace Week11_Assessement.Models;

public class CartLineItemViewModel
{
    public required string Name { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountedPrice { get; set; }
}
