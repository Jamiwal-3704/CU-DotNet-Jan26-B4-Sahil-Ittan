using NorthwindCatalog.Services.DTOs;

namespace NorthwindCatalog.Tests
{
    public class ProductTests
    {
        [Fact]
        public void InventoryValue_Should_Return_Correct_Value()
        {
            var product = new ProductDto
            {
                ProductName = "Chai",
                UnitPrice = 18.00m,
                UnitsInStock = 39
            };

            var result = product.InventoryValue;

            Assert.Equal(702.00m, result);
        }

        [Fact]
        public void InventoryValue_Should_Be_Zero_When_UnitsInStock_Is_Zero()
        {
            var product = new ProductDto
            {
                ProductName = "Out of stock item",
                UnitPrice = 25.50m,
                UnitsInStock = 0
            };

            var result = product.InventoryValue;

            Assert.Equal(0m, result);
        }

        [Fact]
        public void InventoryValue_Should_Handle_Decimal_Price_Precisely()
        {
            var product = new ProductDto
            {
                ProductName = "Decimal price item",
                UnitPrice = 10.25m,
                UnitsInStock = 4
            };

            var result = product.InventoryValue;

            Assert.Equal(41.00m, result);
        }
    }
}
