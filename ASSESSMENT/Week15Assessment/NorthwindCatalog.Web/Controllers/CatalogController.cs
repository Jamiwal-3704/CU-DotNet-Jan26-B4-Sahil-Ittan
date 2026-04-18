using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Web.DTOs;
using NorthwindCatalog.Web.Services;

namespace NorthwindCatalog.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly INorthwindApiClient _api;

        public CatalogController(INorthwindApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index(int? categoryId, string? tab)
        {
            var activeTab = string.Equals(tab, "summary", StringComparison.OrdinalIgnoreCase)
                ? "summary"
                : "catalog";

            var vm = new CategoryIndexViewModel
            {
                Categories = await _api.GetCategoriesAsync(),
                SelectedCategoryId = activeTab == "catalog" ? categoryId : null,
                ActiveTab = activeTab,
                Summaries = await _api.GetCategorySummaryAsync()
            };

            if (vm.SelectedCategoryId.HasValue)
            {
                vm.Products = await _api.GetProductsByCategoryAsync(vm.SelectedCategoryId.Value);
            }

            return View(vm);
        }

        public async Task<IActionResult> Summary()
        {
            return RedirectToAction(nameof(Index), new { tab = "summary" });
        }
    }
}