using Microsoft.AspNetCore.Mvc;
using VoltGearSystems.Models;
using VoltGearSystems.Services;

namespace VoltGearSystems.Controllers
{
    public class LaptopController : Controller
    {
        //static int count = 0;
        private readonly LaptopService _service;

        public LaptopController(LaptopService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var laptops = await _service.GetAsync();
            return View(laptops);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Laptop laptop)
        {
            //count = _service.GetAsync().Result.Count + 1;
            //laptop.Id = count;
            if (!ModelState.IsValid)
                return View(laptop);

            await _service.CreateAsync(laptop);

            TempData["success"] = "Laptop successfully saved to MongoDB";

            return RedirectToAction("Index");
        }
    }
}