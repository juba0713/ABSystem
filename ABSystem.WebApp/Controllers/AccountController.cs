using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, IUserService _userService, UserManager<User> userManager)
        {
            this._signInManager = signInManager;
            this._userService = _userService;
            this._userManager = userManager;
        }

        /*
         * This is to show the login screen.
         */
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return PartialView("~/Views/Login.cshtml");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            if (!ModelState.IsValid)
            {
                ViewData["Error"] = MessageConstant.LOGIN_ERROR;
                return PartialView("~/Views/Login.cshtml");
            }

            var result = await _signInManager.PasswordSignInAsync(dto.Email!, dto.Password!, false, false);
            
            if (!result.Succeeded)
            {

                ViewData["Error"] = MessageConstant.LOGIN_ERROR;
                return PartialView("~/Views/Login.cshtml");
            }

            // Get the logged-in user
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return PartialView("~/Views/Login.cshtml");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return RedirectToAction("AdminDashboard", "Home");
            }
            else if (roles.Contains("Super"))
            {
                return RedirectToAction("SuperDashboard", "Home");
            }

            // Default redirect if no roles match
            return RedirectToAction("UserDashboard", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/register")]
        public IActionResult Register()
        {
            return PartialView("~/Views/Register.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {

            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Register.cshtml");
            }
            try
            {
                await _userService.RegisterUser(dto); 
            }
            catch (Exception ex)
            {
                ViewData["Error"] = MessageConstant.ERROR;
                return PartialView("~/Views/Register.cshtml");
            }

            return RedirectToAction(""); 

        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
