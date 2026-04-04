using Microsoft.AspNetCore.Mvc;
using Vegabond.MVC.Models;
using Vegabond.MVC.Services;

namespace Vegabond.MVC.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly IDestinationService _service;
        private readonly ILogger<DestinationsController> _logger;

        public DestinationsController(IDestinationService service, ILogger<DestinationsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: /Destinations
        public async Task<IActionResult> Index()
        {
            var items = await _service.GetAllAsync();
            return View(items);
        }

        // GET: /Destinations/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /Destinations/Create
        public IActionResult Create()
        {
            return View(new Destination { LastVisited = DateTime.Now });
        }

        // POST: /Destinations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Destination destination)
        {
            if (!ModelState.IsValid) return View(destination);

            try
            {
                await _service.AddAsync(destination);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create failed");
                ModelState.AddModelError(string.Empty, "Failed to create destination.");
                return View(destination);
            }
        }

        // GET: /Destinations/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var item = await _service.GetByIdAsync(id);
        //    if (item == null) return NotFound();
        //    return View(item);
        //}

        // POST: /Destinations/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Destination destination)
        //{
        //    if (id != destination.Id) return BadRequest();

        //    if (!ModelState.IsValid) return View(destination);

        //    try
        //    {
        //        await _service.UpdateAsync(destination);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Update failed");
        //        ModelState.AddModelError(string.Empty, "Failed to update destination.");
        //        return View(destination);
        //    }
        //}

        // GET: /Destinations/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var item = await _service.GetByIdAsync(id);
        //    if (item == null) return NotFound();
        //    return View(item);
        //}

        // POST: /Destinations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        await _service.DeleteAsync(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Delete failed");
        //        return RedirectToAction(nameof(Delete), new { id });
        //    }
        //}
    }
}