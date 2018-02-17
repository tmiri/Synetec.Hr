using Microsoft.AspNetCore.Mvc;
using Synetec.Hr.Core.Dtos.Login;
using Synetec.Hr.Core.Services;
using Synetec.Hr.Web.Models.Login;

namespace Synetec.Hr.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginService _loginService;

        public AccountController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var loginMap = new LoginDto
            {
                UserName = loginViewModel.UserName,
                Password = loginViewModel.Password,
                RememberMe = loginViewModel.RememberMe
            };

            var loginSucceed = _loginService.Login(loginMap);

            if (loginSucceed)
            {
                return RedirectToAction("Dashboard", "Holidays");
            }

            ModelState.AddModelError("", "Invalid login!");

            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            _loginService.Logout();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            _loginService.Dispose();
            base.Dispose(disposing);
        }
    }
}
