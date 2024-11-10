using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/**
 * @Author Julius.B
 * @Added 09/11/2024
 */
namespace ABSystem.WebApp.Controllers
{
    public class AccountController : Controller
    {

        /*
         * This is to show the login screen.
         */
        [HttpGet]
        [AllowAnonymous]
        [Route("/login")]
        public IActionResult Login()
        {
            return PartialView("~/Views/Login.cshtml");
        }

        
    }
}
