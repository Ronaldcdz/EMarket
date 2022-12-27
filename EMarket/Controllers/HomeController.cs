using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Adverts;
using EMarket.Middlewares;
using EMarket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(IAdvertService advertService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _advertService = advertService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index(FilterAdvertViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.Categories = await _categoryService.GetAllViewModel();
            return View(await _advertService.GetAllViewModeWithFilters(vm));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Details(int id, string category)
        {
            return View(await _advertService.GetDetailViewAsync(id, category));
        }
    }
}