namespace Week11_Assessement.Models;

public class ProductPriceViewModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal BasePrice { get; set; }
    public decimal FinalPrice { get; set; }
}
