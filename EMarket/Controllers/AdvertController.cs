using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Adverts;
using EMarket.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Controllers
{
    public class AdvertController : Controller
    {
        private readonly IAdvertService _advertService;
        private readonly ICategoryService _categoryService;
        private readonly ValidateUserSession _validateUserSession;

        public AdvertController(IAdvertService advertService, ICategoryService categoryService, ValidateUserSession validateUserSession)
        {
            _advertService = advertService;
            _categoryService = categoryService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(await _advertService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            SaveAdvertViewModel vm = new();
            vm.Categories = await _categoryService.GetAllViewModel();

            return View("SaveAdvert", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAdvertViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllViewModel();
                return View("SaveAdvert", vm);
            }


            SaveAdvertViewModel advertVm = await _advertService.AddAsync(vm);
            
            if(advertVm != null && advertVm.Id != 0)
            {
                advertVm.ImagePath = UploadFile(vm.File, advertVm.Id);
                await _advertService.UpdateAsync(advertVm); 
            } 

            return RedirectToRoute(new {controller = "Advert", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            var advert = await _advertService.GetByIdSaveViewModel(id);
            advert.Categories = await _categoryService.GetAllViewModel();

            return View("SaveAdvert", advert);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveAdvertViewModel advert)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                advert.Categories = await _categoryService.GetAllViewModel();

                return View("SaveAdvert", advert);

            }



            SaveAdvertViewModel advertVm = await _advertService.GetByIdSaveViewModel(advert.Id);
            advertVm.ImagePath = UploadFile(advert.File, advertVm.Id, true, advertVm.ImagePath);

            await _advertService.UpdateAsync(advertVm);
            return RedirectToRoute(new { controller = "Advert", action = "Index" });
        }



        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            return View(await _advertService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }

            string basePath = $"/Images/Adverts/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Create filder if not exists
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach(FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach(DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }
                Directory.Delete(path);
            }

            await _advertService.DeleteAsync(id);
            return RedirectToRoute(new { controller = "Advert", action = "Index" });
        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if(isEditMode)
            {
                if(file == null)
                {
                    return imagePath;
                }
            }

            //Get directory path
            string basePath = $"/Images/Adverts/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Create filder if not exists
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Get file path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);    

            using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImageName = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);    
                }
            }

            return $"{basePath}/{fileName}";
        }
    }
}
