namespace Week11_Assessement.Models;

public class ProductItem
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal BasePrice { get; set; }
}
