//using Microsoft.AspNetCore.Mvc;

//namespace GlobalMart.Controllers
//{
//    public class CartController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

// using GlobalMart.Models;
// using GlobalMart.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace GlobalMart.Controllers
// {
//     public class CartController : Controller
//     {
//         private readonly IPricingService _pricingService;

//         private static readonly List<CartItem> CartItems = new()
//         {
//             new CartItem { Id = 1, Name = "Laptop", Price = 1000 },
//             new CartItem { Id = 2, Name = "Phone", Price = 500 }
//         };

//         public CartController(IPricingService pricingService)
//         {
//             _pricingService = pricingService;
//         }

//         public IActionResult Index()
//         {
//             const string promoCode = "FREESHIP";

//             var total = CartItems
//                 .Sum(item => _pricingService.CalculateTotalPrice(item.Price, promoCode));

//             ViewBag.Total = total;

//             return View(CartItems);
//         }

//         public IActionResult Checkout()
//         {
//             return RedirectToAction(nameof(Index));
//         }

//         public IActionResult Details(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var cartItem = CartItems.FirstOrDefault(c => c.Id == id.Value);
//             if (cartItem == null)
//             {
//                 return NotFound();
//             }

//             return View(cartItem);
//         }

//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Create(CartItem cartItem)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return View(cartItem);
//             }

//             var newId = CartItems.Count == 0 ? 1 : CartItems.Max(c => c.Id) + 1;
//             cartItem.Id = newId;
//             CartItems.Add(cartItem);

//             return RedirectToAction(nameof(Index));
//         }

//         public IActionResult Edit(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var cartItem = CartItems.FirstOrDefault(c => c.Id == id.Value);
//             if (cartItem == null)
//             {
//                 return NotFound();
//             }

//             return View(cartItem);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public IActionResult Edit(int id, CartItem cartItem)
//         {
//             if (id != cartItem.Id)
//             {
//                 return NotFound();
//             }

//             if (!ModelState.IsValid)
//             {
//                 return View(cartItem);
//             }

//             var existing = CartItems.FirstOrDefault(c => c.Id == id);
//             if (existing == null)
//             {
//                 return NotFound();
//             }

//             existing.Name = cartItem.Name;
//             existing.Price = cartItem.Price;

//             return RedirectToAction(nameof(Index));
//         }

//         public IActionResult Delete(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var cartItem = CartItems.FirstOrDefault(c => c.Id == id.Value);
//             if (cartItem == null)
//             {
//                 return NotFound();
//             }

//             return View(cartItem);
//         }

//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public IActionResult DeleteConfirmed(int id)
//         {
//             var cartItem = CartItems.FirstOrDefault(c => c.Id == id);
//             if (cartItem != null)
//             {
//                 CartItems.Remove(cartItem);
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
    public class CartController : Controller
    {
        private readonly IPricingService _pricingService;

        private static readonly List<CartItem> CartItems = new()
        {
            new CartItem { Id = 1, Name = "Laptop", Price = 1000m },
            new CartItem { Id = 2, Name = "Phone", Price = 500m }
        };

        public CartController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public IActionResult Index()
        {
            const string promoCode = "FREESHIP";

            var total = CartItems
                .Sum(item => _pricingService.CalculateTotalPrice(item.Price, promoCode));

            ViewBag.Total = total;
            ViewBag.Promo = promoCode;

            return View(CartItems);
        }

        public IActionResult Checkout()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var cartItem = CartItems.FirstOrDefault(c => c.Id == id.Value);
            if (cartItem == null) return NotFound();

            return View(cartItem);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CartItem cartItem)
        {
            if (!ModelState.IsValid) return View(cartItem);

            var newId = CartItems.Count == 0 ? 1 : CartItems.Max(c => c.Id) + 1;
            cartItem.Id = newId;
            CartItems.Add(cartItem);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var cartItem = CartItems.FirstOrDefault(c => c.Id == id.Value);
            if (cartItem == null) return NotFound();

            return View(cartItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CartItem cartItem)
        {
            if (id != cartItem.Id) return NotFound();
            if (!ModelState.IsValid) return View(cartItem);

            var existing = CartItems.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.Name = cartItem.Name;
            existing.Price = cartItem.Price;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var cartItem = CartItems.FirstOrDefault(c => c.Id == id.Value);
            if (cartItem == null) return NotFound();

            return View(cartItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cartItem = CartItems.FirstOrDefault(c => c.Id == id);
            if (cartItem != null) CartItems.Remove(cartItem);

            return RedirectToAction(nameof(Index));
        }
    }
}