namespace NorthwindCatalog.Web.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public decimal InventoryValue => UnitPrice * UnitsInStock;
    }
}
