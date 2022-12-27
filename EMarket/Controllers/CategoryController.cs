using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.Services;
using EMarket.Core.Application.ViewModels.Adverts;
using EMarket.Core.Application.ViewModels.Categories;
using EMarket.Core.Domain.Entities;
using EMarket.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _categoryService = categoryService;
            _validateUserSession = validateUserSession; 
        }



        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _categoryService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View("SaveCategory", new SaveCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCategoryViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCategory", vm);
            }

            await _categoryService.AddAsync(vm);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var category = await _categoryService.GetByIdSaveViewModel(id);

            return View("SaveCategory", category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveCategoryViewModel category)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View("SaveCategory", category);

            }

            await _categoryService.UpdateAsync(category);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _categoryService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            await _categoryService.DeleteAsync(id);
            return RedirectToRoute(new { controller = "Category", action = "Index" });
        }
    }
}
