// //using Microsoft.AspNetCore.Mvc;

// //namespace GlobalMart.Controllers
// //{
// //    public class ProductsController : Controller
// //    {
// //        public IActionResult Index()
// //        {
// //            return View();
// //        }
// //    }
// //}

// using GlobalMart.Models;
// using GlobalMart.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace GlobalMart.Controllers
// {
//     public class ProductsController : Controller
//     {
//         private readonly IPricingService _pricingService;

//         private static readonly List<Product> Products = new List<Product>()
//         {
//             new Product { ProductId = 1, ProductName = "Laptop", Price = 100000 },
//             new Product { ProductId = 2, ProductName = "Phone", Price = 50500 },
//             new Product { ProductId = 3, ProductName = "Headphones", Price = 76200 }
//         };

//         public ProductsController(IPricingService pricingService)
//         {
//             _pricingService = pricingService;
//         }

//         public IActionResult Index()
//         {
//             var promocode1 = "FREESHIP";
//             var promoCode2 = "WINTER25";
//             Console.WriteLine("Seelct any one PromoCode: ");
//             var input = int.Parse(Console.ReadLine());

//             if(input == 1)
//             { 
//                 var discountedProducts = Products
//                     .Select(item => new Product
//                     {
//                         ProductId = item.ProductId,
//                         ProductName = item.ProductName,
//                         Price = _pricingService.CalculateTotalPrice(item.Price, promocode1)
//                     })
//                     .ToList();
//                 ViewBag.Price = "FREESHIP applied";
//                 return View(discountedProducts);
//             }

//             else
//             {
//                 var discountedProducts = Products
//                 .Select(item => new Product
//                 {
//                     ProductId = item.ProductId,
//                     ProductName = item.ProductName,
//                     Price = _pricingService.CalculateTotalPrice(item.Price, promoCode2)
//                 })
//                 .ToList();

//                 ViewBag.Price = "WINTER25 applied";

//                 return View(discountedProducts);

//             }
//         }

//         public IActionResult Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var product = Products.FirstOrDefault(p => p.ProductId == id.Value);
//             if (product == null)
//             {
//                 return NotFound();
//             }

//             return View(product);
//         }

//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Create(Product product)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return View(product);
//             }

//             var newId = Products.Count == 0 ? 1 : Products.Max(p => p.ProductId) + 1;
//             product.ProductId = newId;
//             Products.Add(product);

//             return RedirectToAction(nameof(Index));
//         }

//         public IActionResult Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var product = Products.FirstOrDefault(p => p.ProductId == id.Value);
//             if (product == null)
//             {
//                 return NotFound();
//             }

//             return View(product);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Edit(int id, Product product)
//         {
//             if (id != product.ProductId)
//             {
//                 return NotFound();
//             }

//             if (!ModelState.IsValid)
//             {
//                 return View(product);
//             }

//             var existing = Products.FirstOrDefault(p => p.ProductId == id);
//             if (existing == null)
//             {
//                 return NotFound();
//             }

//             existing.ProductName = product.ProductName;
//             existing.Price = product.Price;

//             return RedirectToAction(nameof(Index));
//         }

//         public IActionResult Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var product = Products.FirstOrDefault(p => p.ProductId == id.Value);
//             if (product == null)
//             {
//                 return NotFound();
//             }

//             return View(product);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public IActionResult DeleteConfirmed(int id)
//         {
//             var product = Products.FirstOrDefault(p => p.ProductId == id);
//             if (product != null)
//             {
//                 Products.Remove(product);
//             }

//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
using GlobalMart.Models;
using GlobalMart.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IPricingService _pricingService;

        private static readonly List<Product> Products = new()
        {
            new Product { ProductId = 1, ProductName = "Laptop", Price = 100000m },
            new Product { ProductId = 2, ProductName = "Phone", Price = 50500m },
            new Product { ProductId = 3, ProductName = "Headphones", Price = 76200m }
        };

        public ProductsController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public IActionResult Index()
        {
            const string promoCode = "WINTER25";

            var discountedProducts = Products
                .Select(item => new Product
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = _pricingService.CalculateTotalPrice(item.Price, promoCode)
                })
                .ToList();

            ViewBag.Price = "WINTER25 applied";

            return View(discountedProducts);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var product = Products.FirstOrDefault(p => p.ProductId == id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            var newId = Products.Count == 0 ? 1 : Products.Max(p => p.ProductId) + 1;
            product.ProductId = newId;
            Products.Add(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = Products.FirstOrDefault(p => p.ProductId == id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return NotFound();
            if (!ModelState.IsValid) return View(product);

            var existing = Products.FirstOrDefault(p => p.ProductId == id);
            if (existing == null) return NotFound();

            existing.ProductName = product.ProductName;
            existing.Price = product.Price;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = Products.FirstOrDefault(p => p.ProductId == id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = Products.FirstOrDefault(p => p.ProductId == id);
            if (product != null) Products.Remove(product);

            return RedirectToAction(nameof(Index));
        }
    }
}