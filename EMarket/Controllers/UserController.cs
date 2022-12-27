using EMarket.Core.Application.Interfaces.Services;
using EMarket.Core.Application.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using EMarket.Core.Application.Helpers;
using EMarket.Core.Domain.Entities;
using EMarket.Middlewares;

namespace EMarket.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;


        public UserController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginVm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UserViewModel userVm = await _userService.Login(loginVm);
            if(userVm != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVm);

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            else
            {
                ModelState.AddModelError("userValidation", "Incorrect login data");
            }
            return View();
        }

        public IActionResult Register()
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userVm)
        {
            if (_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                return View(userVm);
            }

            if (await _userService.UserExists(userVm) == true) 
            {
                userVm.Username = "";
                ViewBag.Error = "The username already exists, please choose another";
                return View(userVm);
            }

            await _userService.AddAsync(userVm);
            return RedirectToRoute(new { controller = "User", action = "Index"});
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });

        }

    }
}
