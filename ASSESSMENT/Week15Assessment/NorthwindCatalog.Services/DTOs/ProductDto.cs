namespace NorthwindCatalog.Services.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }

        // 🔥 Calculated Property (IMPORTANT)
        public decimal InventoryValue => UnitPrice * UnitsInStock;
    }
}
